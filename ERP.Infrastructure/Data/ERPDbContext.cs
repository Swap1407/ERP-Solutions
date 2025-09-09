using ERP.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.Data
{
    public class ERPDbContext : DbContext
    {
        public ERPDbContext(DbContextOptions contextOptions): base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
