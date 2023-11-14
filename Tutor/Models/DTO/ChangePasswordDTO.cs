using System.ComponentModel.DataAnnotations;

namespace Tutor.Models.DTO
{
    public class ChangePasswordDTO
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required] [MinLength(8)] public string NewPassword {  get; set; }

        [Required]
        [MinLength(8)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set;}
    }
}
