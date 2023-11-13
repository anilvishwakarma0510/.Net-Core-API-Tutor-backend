using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Tutor.Models.Domain
{
    public class UserModel
    { 
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
   
        public string FirstName { get; set; }

        [Required]
        [StringLength (100)]
        public string LastName { get; set; }

        [Required]
        [StringLength (100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = null;

        [Required]
        [StringLength (100)]
        public string Password { get; set; }

        [StringLength(255)]
        [AllowNull]
        public string? ProfileImage { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public bool EmailVerified {  get; set; } = false;

        [Required]
        public bool Status { get; set; } = true;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //Navigate role property
        public RoleModel UserRole { get; set; }

    }
}
