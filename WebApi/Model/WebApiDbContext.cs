using Microsoft.EntityFrameworkCore;

namespace WebApi.Model
{
    public class WebApiDbContext:DbContext
    {
        public WebApiDbContext(DbContextOptions options):base(options)
        {
            
        }

       public DbSet<Student>students { get; set; }
       public DbSet<Login>logins { get; set; }
    }
}
