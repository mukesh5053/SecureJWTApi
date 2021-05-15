using Microsoft.EntityFrameworkCore;
using SecureJWTApi.DataAccess.Infrastructure;

namespace SecureJWTApi.DataAccess.DataContext
{
    public class SecureDBContext : DbContext
    {
        public SecureDBContext(DbContextOptions<SecureDBContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        
        }

    }
}
