namespace Tutor.Models.DTO
{
    public class StudentDTO : UserDTO
    {

        public RoleDTO UserRole { get; set; }

        public string? ProfileImage { get; set; }

        public bool EmailVerified { get; set; }

        public bool Status { get; set;}

    }
}
