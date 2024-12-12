using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Models
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
