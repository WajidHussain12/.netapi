using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly WebApiDbContext _dbContext;
        public loginController(IConfiguration configuration, WebApiDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Student student)
        {
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(student);
            if (user != null)
            {
                var token = generateToken(student);
                response = Ok(new { token = token });
            }
            return response;
        }
        private Student AuthenticateUser(Student student)    
        {
            Student authenticatedStudent = null;

            // Assuming there is a DbSet<Student> in your _dbContext named "students"
            var foundStudent = _dbContext.students.FirstOrDefault(s => s.name == student.name);

            if (foundStudent != null)
            {
                // Authentication logic, e.g., check password or other credentials
                // For now, let's assume a simple check on the name for illustration
                authenticatedStudent = new Student { name = "user" };
            }

            return authenticatedStudent;
        }


        private string generateToken(Student student)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }





    }

}
