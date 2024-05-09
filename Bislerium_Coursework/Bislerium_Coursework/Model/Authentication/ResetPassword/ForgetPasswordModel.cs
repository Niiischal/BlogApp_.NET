using Microsoft.AspNetCore.Identity;

namespace Bislerium_Coursework.Model.Authentication.ResetPassword
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }

    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Code { get; set; }  // This replaces the Token
        public string NewPassword { get; set; }
    }

    public class ResetPasswordCode
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual IdentityUser User { get; set; }
    }



}
