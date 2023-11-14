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
            var user = await _db.Users.Include(u => u.UserRole).SingleOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return Unauthorized(
                    new
                    {
                        message = "1Invalid details"
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

            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value}/";


            if (user.ProfileImage == null)
            {
                user.ProfileImage = baseUrl + "user-profile/default-profile.png";
            }
            else
            {
                user.ProfileImage = baseUrl + user.ProfileImage;
            }


            UserDTO userDto = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailVerified = user.EmailVerified,
                Status = user.Status,
                ProfileImage = user.ProfileImage, // Get the profile image URL
                UserRole = new RoleDTO
                {
                    Id = user.UserRole.Id,
                    Name = user.UserRole.Name
                }
            };
            return Ok(new
            {
                message = "Success",
                user = userDto,
                token = token
            });

        }

  
        [HttpGet("get-user/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _db.Users.Include(u => u.UserRole).FirstOrDefaultAsync(u => u.Id == id);
            //Console.WriteLine("---------------SART--------------");
            //Console.WriteLine(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            //Console.WriteLine("---------------END--------------");

            if (user == null)
            {
                return NotFound(new { message = "User not found"} ); // User not found
            }

            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value}/";

            if (user.ProfileImage == null)
            {
                user.ProfileImage = baseUrl + "user-profile/default-profile.png";
            }
            else
            {
                user.ProfileImage = baseUrl + user.ProfileImage;
            }

            UserDTO userDto = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailVerified = user.EmailVerified,
                Status = user.Status,
                ProfileImage = user.ProfileImage, // Get the profile image URL
                UserRole = new RoleDTO
                {
                    Id = user.UserRole.Id,
                    Name = user.UserRole.Name
                }
            };
            return Ok(new
            {
                message = "Success",
                user = userDto
            });
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

                var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value}/";

                if (user.ProfileImage == null)
                {
                    user.ProfileImage = baseUrl + "user-profile/default-profile.png";
                }
                else
                {
                    user.ProfileImage = baseUrl + user.ProfileImage;
                }



                UserDTO userDto = new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailVerified = user.EmailVerified,
                    Status = user.Status,
                    ProfileImage = user.ProfileImage, // Get the profile image URL
                    UserRole = new RoleDTO
                    {
                        Id = user.UserRole.Id,
                        Name = user.UserRole.Name
                    }
                };
                return Ok(new
                {
                    message = "Success",
                    user = userDto
                });


            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("edit-profile")]
        [Authorize]
        public async Task<IActionResult> EditProfile([FromForm] EditProfileDTO req)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email)?.Value;

                var user = await _db.Users.Include(x => x.UserRole).FirstOrDefaultAsync(u => u.Email == Email);

                if (user == null)
                {
                    return NotFound(new { message = "User not found"});
                }

                if (!string.IsNullOrEmpty(req.Email)){
                    if(user.RoleId == 1 && user.Email != req.Email)
                    {
                        if(await _db.Users.AnyAsync(u => u.Email == req.Email && u.Id != user.Id)) {
                            user.Email = req.Email;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(req.PhoneNumber)) {
                    if(user.PhoneNumber != req.PhoneNumber)
                    {
                        if(await _db.Users.AnyAsync(u => u.PhoneNumber == req.PhoneNumber && u.Id != user.Id))
                        {
                            user.PhoneNumber = req.PhoneNumber;
                        }
                    }
                }

                user.FirstName = req.FirstName;
                user.LastName = req.LastName;

                if (req.ProfileImage != null && req.ProfileImage.Length > 0) {
                    var uniqueName = Guid.NewGuid().ToString()+"_"+req.ProfileImage.FileName;
                    var filePath = Path.Combine("wwwroot/user-profile",uniqueName);

                    using(var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await req.ProfileImage.CopyToAsync(fileStream);
                    }

                    user.ProfileImage = "user-profile/" + uniqueName;

                }

                await _db.SaveChangesAsync();

                user = await _db.Users.Include(u => u.UserRole).FirstOrDefaultAsync(u => u.Id == user.Id);

                var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value}/";

                if (user.ProfileImage == null)
                {
                    user.ProfileImage = baseUrl + "user-profile/default-profile.png";
                }
                else
                {
                    user.ProfileImage = baseUrl + $"{user.ProfileImage}";
                }

                UserDTO userDto = new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailVerified = user.EmailVerified,
                    Status = user.Status,
                    ProfileImage = user.ProfileImage, // Get the profile image URL
                    UserRole = new RoleDTO
                    {
                        Id = user.UserRole.Id,
                        Name = user.UserRole.Name
                    }
                };



                return Ok(new
                {
                    status=1,
                    message="Profile has been updated successfully.",
                    user= userDto
                });
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private string GenerateToken(UserModel user)
        {

            Console.WriteLine("---------------START ROLE--------------");
            Console.WriteLine(user.UserRole.Name);
            Console.WriteLine("---------------END ROLE--------------");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,user.UserRole.Name),
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