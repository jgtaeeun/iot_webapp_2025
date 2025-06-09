using Microsoft.EntityFrameworkCore;

namespace WebApiApp03.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDBContext()
        {
        }

        public DbSet<IOTDatas> IOT_DATA { get; set; }
    }
}
