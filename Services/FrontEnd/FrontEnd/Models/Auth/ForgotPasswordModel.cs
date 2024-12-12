using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models.Auth
{
    public class ForgotPasswordModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
