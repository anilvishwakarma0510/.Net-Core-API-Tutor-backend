using System.ComponentModel.DataAnnotations;

namespace Tutor.Models.DTO
{
    public class AddUserDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required] public string LastName { get;set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required] [MinLength(8)] public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required] [MinLength(10)] public string PhoneNumber { get; set; }

        public string? ProfileImage { get; set;  }

        [Required] public int RoleId { get; set; }


    }
}
