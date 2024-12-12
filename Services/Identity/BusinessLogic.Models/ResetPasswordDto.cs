using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models
{
    public class ResetPasswordDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password should contain minimum 6 symbols.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords are not the same")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
