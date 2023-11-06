using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutor.Data;
using Tutor.Models.DTO;
using Tutor.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Tutor.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly TutorDbContext _tutorDbContext;

        public AdminController(TutorDbContext tutorDbContext)
        {
            _tutorDbContext = tutorDbContext;
        }

        [HttpGet]
        public IActionResult GetAll() {
            return Ok("Hello");
        }

        
    }
}
