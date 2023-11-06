namespace Tutor.Models.DTO
{
    public class TutorDTO : UserDTO
    {
        public RoleDTO UserRole { get; set; }

        public string? ProfileImage { get; set; }

        public bool EmailVerified { get; set; }

        public bool Status { get; set; }
    }
}
