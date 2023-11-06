namespace Tutor.Models.DTO
{
    public class AdminDTO : UserDTO
    {
        public RoleDTO UserRole { get; set; }

        public string? ProfileImage { get; set; }
    }
}
