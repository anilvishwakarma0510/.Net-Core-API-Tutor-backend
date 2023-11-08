using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutor.Data;
using Tutor.Models.Domain;
using Tutor.Models.DTO;

namespace Tutor.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TutorDbContext _db;

        public UserController(TutorDbContext tutorDbContext)
        {
            this._db = tutorDbContext;
        }

        [HttpPost("add-user")]
        [Authorize]

        public async Task<IActionResult> AddUser([FromBody] AddUserDTO request)
        {
            try
            {
                if (request.ConfirmPassword != request.Password)
                {
                    return BadRequest(new { message = "The password and confirmation password do not match." });
                }

                if (await _db.Users.AnyAsync(u => u.Email == request.Email))
                {
                    return BadRequest(new { message = "Email is not unique" });
                }

                if (await _db.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber))
                {
                    return BadRequest(new { message = "Phone number is not unique" });
                }



                var newUser = new UserModel
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    PhoneNumber = request.PhoneNumber,
                    ProfileImage = request.ProfileImage,
                    RoleId = request.RoleId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                // Save the new user to the database
                _db.Users.Add(newUser);
                await _db.SaveChangesAsync();

                //await SendConfirmationEmail(newUser.Email, confirmationLink);

                newUser = await _db.Users.Include(u => u.UserRole).FirstOrDefaultAsync(u => u.Id == newUser.Id);

              
                UserDTO userDto = new UserDTO
                {
                    Id = newUser.Id,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    EmailVerified = newUser.EmailVerified,
                    Status = newUser.Status,
                    ProfileImage = newUser.ProfileImage, // Get the profile image URL
                    UserRole = new RoleDTO
                    {
                        Id = newUser.UserRole.Id,
                        Name = newUser.UserRole.Name
                    }
                };
                return Ok(new
                {
                    message = "User added successfully",
                    user = userDto
                });
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("edit-user/{id}")]
        [Authorize]
        public async Task<IActionResult> EditUser(int id, [FromBody] EditUserDTO request)
        {
            try
            {
                UserModel user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return NotFound(new { message = "User not found"});
                }

                if(await _db.Users.AnyAsync(u => u.Email == request.Email && u.Id != id))
                {
                    return BadRequest(new { message = "Email is not unique" });
                }

                if (await _db.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber && u.Id != id))
                {
                    return BadRequest(new { message = "Phone number is not unique" });
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                user.PhoneNumber = request.PhoneNumber;
                user.ProfileImage = request.ProfileImage;
                user.RoleId = request.RoleId;
                user.UpdatedAt = DateTime.Now;

                if (!string.IsNullOrEmpty(request.Password))
                {
                    if (request.ConfirmPassword != request.Password)
                    {
                        return BadRequest(new { message = "The password and confirmation password do not match." });
                    }

                    user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                }

                await _db.SaveChangesAsync();

                user = await _db.Users.Include(u => u.UserRole).FirstOrDefaultAsync(u => u.Id == id);

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
                    message = "User updated successfully",
                    user = userDto
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-users")]
        [Authorize]
        public async Task<IActionResult> GetAllUser([FromQuery] SearchDTO req)
        {
            try
            {
                var query = _db.Users
                        .Include(u => u.UserRole)
                        .Where(u =>
                            (req.search == null || u.FirstName.Contains(req.search) || u.LastName.Contains(req.search) || u.Email.Contains(req.search)) &&
                            (!req.role.HasValue || u.RoleId == req.role) &&
                            (string.IsNullOrEmpty(req.search) || u.UserRole.Name == req.search) &&
                            u.RoleId != 1);

                int totalUsers = await query.CountAsync();

                int offset = (int)((req.page - 1) * req.limit);
                
                

                var users = await query.OrderByDescending(u => u.Id)
                    .Skip(offset)
                    .Take((int)req.limit)
                    .ToListAsync();

                var userDtos = users.Select(user => new UserDTO
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
                }).ToArray();

                return Ok(new
                {
                    status = 1,
                    message = "Success",
                    users = users
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
 