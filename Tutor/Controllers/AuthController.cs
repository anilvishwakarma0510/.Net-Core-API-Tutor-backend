using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutor.Data;
using Tutor.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Tutor.Models.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Tutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TutorDbContext _db;
        private readonly IConfiguration _configuration;
        

        public AuthController(TutorDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return Unauthorized(
                    new
                    {
                        message = "Invalid details"
                    }
                );
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized(
                    new
                    {
                        message = "Invalid details"
                    }
                );
            }

            var token = GenerateToken(user);

            var userDto = new StudentDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return Ok(new { token, user = userDto });

        }

  
        [HttpGet("get-user/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { message = "User not found"} ); // User not found
            }

            var userDto = new StudentDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                // Add other user properties as needed
            };

            return Ok(userDto);
        }

        [HttpGet("get-user")]
        [Authorize]
        public async Task<IActionResult> GetUserByToken()
        {
            try
            {


                var Email = User.FindFirst(ClaimTypes.Email)?.Value;

                var user = await _db.Users.Include(u => u.UserRole).FirstOrDefaultAsync(u => u.Email == Email);

                if (user == null)
                {
                    return NotFound(new { message = "User not found" }); // User not found
                }

                UserDTO userDto = null;
                if(user.RoleId == 1)
                {
                    userDto = new AdminDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        UserRole = new RoleDTO
                        {
                            Id = user.UserRole.Id,
                            Name = user.UserRole.Name
                        }
                    };

                } 
                else if (user.RoleId == 3)
                {
                    userDto = new TutorDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        EmailVerified = user.EmailVerified,
                        Status = user.Status,
                        UserRole = new RoleDTO
                        {
                            Id = user.UserRole.Id,
                            Name = user.UserRole.Name
                        }
                    };

                } else if (user.RoleId == 4)
                {
                    userDto = new StudentDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        EmailVerified = user.EmailVerified,
                        Status = user.Status,
                        UserRole = new RoleDTO
                        {
                            Id = user.UserRole.Id,
                            Name = user.UserRole.Name
                        }
                    };

                }



                return Ok(userDto);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GenerateToken(UserModel user)
        {


           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
                // Add other claims as needed
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}