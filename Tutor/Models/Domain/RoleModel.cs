using System.ComponentModel.DataAnnotations;

namespace Tutor.Models.Domain
{
    public class RoleModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}

        public ICollection<UserModel> Users { get; set; }
    }
}
