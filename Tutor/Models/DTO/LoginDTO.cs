using System.Diagnostics.CodeAnalysis;

namespace Tutor.Models.DTO
{
    public class LoginDTO
    {
        //[Required]
        [NotNull]
        public string Email {  get; set; }

        //[Required]
        [NotNull]
        public string Password { get; set; }

    }
}
