using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Migrations;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        

        private readonly WebApiDbContext _Context;
        public StudentApiController(WebApiDbContext Context)
        {
            _Context = Context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Student>>> getStdData()
        {
            var data = await _Context.students.ToListAsync();
            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> getStdByid(int id)
        {
            var data = await _Context.students.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }


        [HttpPost]

        public async Task<ActionResult<Student>> createStdData(Student StdData)
        {
            await _Context.students.AddAsync(StdData);
            await _Context.SaveChangesAsync();
            return Ok(StdData);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> upDateStd(int id, Student std)
        {
            if (id != std.id)
            {
                return BadRequest();

            }
            _Context.Entry(std).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> deleteStd(int id)
        {
            var std = await _Context.students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }

            _Context.students.Remove(std);
            _Context.SaveChanges();
            return Ok();
        }

    }
}
