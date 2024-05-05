using Bislerium_Coursework.Model.Authentication;
using Bislerium_Coursework.Model.Authentication.Login;
using Bislerium_Coursework.Model.Authentication.Signup;
using Bislerium_Coursework_Service.Model;
using Bislerium_Coursework_Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium_Coursework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthenticationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            // Check if the email already exists
            var userByEmail = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userByEmail != null)
            {
                return Conflict(new Response { Status = "Error", Message = "Email already exists!" });
            }

            // Check if the username already exists
            var userByUsername = await _userManager.FindByNameAsync(registerUser.Username);
            if (userByUsername != null)
            {
                return Conflict(new Response { Status = "Error", Message = "Username already exists!" });
            }

            // Adding new User
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

            // Assign default role
            await _userManager.AddToRoleAsync(user, "User");

            // Generate the email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Create an absolute URL for the confirmation link
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);

            // Prepare and send the confirmation email
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
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
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
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }

            return Unauthorized(new Response { Status = "Error", Message = "Username or password is incorrect" });
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
    }
}
