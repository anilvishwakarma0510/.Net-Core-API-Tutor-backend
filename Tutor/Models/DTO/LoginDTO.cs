using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Tutor.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [NotNull]
        public string Email {  get; set; }

        [Required]
        public string Password { get; set; }

    }
}
