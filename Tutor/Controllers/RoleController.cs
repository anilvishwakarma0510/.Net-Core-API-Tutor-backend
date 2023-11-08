using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutor.Data;
using Tutor.Models.DTO;

namespace Tutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly TutorDbContext _db;

        public RoleController(TutorDbContext tutorDbContext)
        {
            this._db = tutorDbContext;
        }

        [HttpGet("get-roles")]
        public async Task<IActionResult> RetRoles() { 
            try
            {
                var data = await _db.Role.Where(r => r.Id != 1).ToListAsync();

                var rolesData = data.Select(role => new RoleDTO { Id = role.Id, Name = role.Name }).ToArray();


                return Ok(rolesData);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
