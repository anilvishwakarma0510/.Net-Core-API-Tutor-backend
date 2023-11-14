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
    [Authorize(Roles = "Administrator")]
    public class UserController : ControllerBase
    {
        private readonly TutorDbContext _db;

        public UserController(TutorDbContext tutorDbContext)
        {
            this._db = tutorDbContext;
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser([FromForm] AddUserDTO request)
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

                string ProfileImage = null;

                if (request.ProfileImage != null && request.ProfileImage.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + request.ProfileImage.FileName;
                    var filePath = Path.Combine("wwwroot/user-profile", uniqueFileName);   
                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.ProfileImage.CopyToAsync(fileStream);
                    }

                    ProfileImage = "user-profile/" + uniqueFileName;
                }

                var newUser = new UserModel
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    PhoneNumber = request.PhoneNumber,
                    ProfileImage = ProfileImage,
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
        public async Task<IActionResult> EditUser(int id, [FromForm] EditUserDTO request)
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


                if (request.ProfileImage != null && request.ProfileImage.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + request.ProfileImage.FileName;
                    var filePath = Path.Combine("wwwroot/user-profile", uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.ProfileImage.CopyToAsync(fileStream);
                    }

                    user.ProfileImage = "user-profile/" + uniqueFileName;
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
        public async Task<IActionResult> GetAllUser([FromQuery] SearchDTO req)
        {
            try
            {
                var query = _db.Users
            .Include(u => u.UserRole)
            .Where(u => u.RoleId != 1);

                if (!string.IsNullOrEmpty(req.search))
                {
                    query = query.Where(u =>
                        EF.Functions.Like(u.FirstName, "%" + req.search + "%") ||
                        EF.Functions.Like(u.LastName, "%" + req.search + "%") ||
                        EF.Functions.Like(u.Email, "%" + req.search + "%") ||
                        EF.Functions.Like(u.UserRole.Name, "%" + req.search + "%%"));
                }

                if (req.role.HasValue)
                {
                    query = query.Where(u => u.RoleId == req.role);
                }


                int totalUsers = await query.CountAsync();

                int offset = req.page.HasValue ? (int)((req.page.Value - 1) * req.limit.GetValueOrDefault()) : 0;

                var users = await query.OrderByDescending(u => u.Id)
                    .Skip(offset)
                    .Take(req.limit.GetValueOrDefault())
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
                    users = userDtos // Use userDtos instead of users
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
 