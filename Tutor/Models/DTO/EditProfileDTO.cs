using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Tutor.Models.DTO
{
    public class EditProfileDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required][EmailAddress] public string? Email { get; set; }

        [Required][MinLength(10)] public string PhoneNumber { get; set; }

        [AllowNull] public IFormFile? ProfileImage { get; set; }
    }
}
