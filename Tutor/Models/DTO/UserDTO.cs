namespace Tutor.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? ProfileImage { get; set; }
        public RoleDTO UserRole { get; set; }

        public string? PhoneNumber { get; set; }

        public bool EmailVerified { get; set; }

        public bool Status { get; set; }
    }
}
