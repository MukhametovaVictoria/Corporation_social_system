using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
