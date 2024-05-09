using Bislerium_Coursework.Data;
using Bislerium_Coursework.Model.Authentication;
using Bislerium_Coursework.Model.Authentication.Login;
using Bislerium_Coursework.Model.Authentication.ResetPassword;
using Bislerium_Coursework.Model.Authentication.Signup;
using Bislerium_Coursework_Service.Model;
using Bislerium_Coursework_Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bislerium_Coursework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private static Dictionary<string, (string Code, DateTime Expiration)> _resetCodes = new Dictionary<string, (string Code, DateTime Expiration)>();

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly AuthDbContext _context;

        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            IEmailService emailService,
            AuthDbContext context,
            ILogger<AuthenticationController> logger)  
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
            _context = context;  // Initialize the context
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var userByEmail = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userByEmail != null)
            {
                return Conflict(new Response { Status = "Error", Message = "Email already exists!" });
            }

            var userByUsername = await _userManager.FindByNameAsync(registerUser.Username);
            if (userByUsername != null)
            {
                return Conflict(new Response { Status = "Error", Message = "Username already exists!" });
            }

            IdentityUser user = new IdentityUser
            {
                UserName = registerUser.Username,
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed: " + errors });
            }

            await _userManager.AddToRoleAsync(user, "User");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email }, "Confirm your account", $"Please confirm your account by clicking this link: {confirmationLink}");
            _emailService.SendEmail(message);

            return StatusCode(StatusCodes.Status201Created, new Response { Status = "Success", Message = $"User created successfully and verification email sent to {user.Email}" });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new Response { Status = "Error", Message = "User not found" });
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new Response { Status = "Success", Message = "Email verified successfully" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Email could not be verified" });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                return Unauthorized(new Response { Status = "Error", Message = "Username or password is incorrect" });
            }

            if (!user.EmailConfirmed)
            {
                return Unauthorized(new Response { Status = "Error", Message = "Email has not been verified. Please check your email to confirm your account." });
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = GetToken(authClaims);
            return Ok(new
            {
                message = "Login successful",
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = jwtToken.ValidTo
            });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest(new Response { Status = "Error", Message = "Email is required" });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound(new Response { Status = "Error", Message = "User not found" });
            }

            // Generate reset token
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Save the token with expiry in the database (optional: handle expiry manually)
            var resetEntry = new ResetPasswordCode
            {
                UserId = user.Id,
                Code = resetToken,
                ExpiryDate = DateTime.UtcNow.AddHours(1) // Token expiry time
            };

            _context.ResetPasswordCodes.Add(resetEntry);
            await _context.SaveChangesAsync();

            // Send email with the token
            var emailMessage = $"Your password reset code is: {resetToken}";
            _emailService.SendEmail(new Message(new string[] { user.Email }, "Reset Your Password", emailMessage));

            return Ok(new Response { Status = "Success", Message = "Password reset token has been sent to your email address." });
        }



        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new Response { Status = "Error", Message = "User not found" });
            }

            // Validate the reset token
            var resetEntry = await _context.ResetPasswordCodes.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Code == model.Code && x.ExpiryDate > DateTime.UtcNow);
            if (resetEntry == null)
            {
                return BadRequest(new Response { Status = "Error", Message = "Invalid or expired reset code" });
            }

            // Reset the password
            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Code, model.NewPassword);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description).ToArray();
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join(", ", errors) });
            }

            // Remove the reset token entry from the database
            _context.ResetPasswordCodes.Remove(resetEntry);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Password has been reset successfully." });
        }



    }
}
