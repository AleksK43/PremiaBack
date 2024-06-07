using Microsoft.EntityFrameworkCore;
using Premia_API.Entities;

namespace Premia_API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Premia_API.Entities.Customer> Customer { get; set; }
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Document> Documents { get; set; }
        public DbSet<Entities.BonusTask> BonusTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerUser>()
                .HasKey(cu => new { cu.CustomerId, cu.UserId });

            modelBuilder.Entity<CustomerUser>()
                .HasOne(cu => cu.Customer)
                .WithMany(c => c.CustomerUsers)
                .HasForeignKey(cu => cu.CustomerId);

            modelBuilder.Entity<CustomerUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.CustomerUsers)
                .HasForeignKey(cu => cu.UserId);
        }


    }
}
