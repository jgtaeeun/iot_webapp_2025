using Microsoft.EntityFrameworkCore;

namespace WebApiApp02.Models
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected ApplicationDBContext()
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
