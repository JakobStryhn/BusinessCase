using Core.Domain.Models;
using Core.Features.Office.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence
{
    public class DBContext(DbContextOptions<DBContext> options) : DbContext(options)
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<OfficeEntity> Offices { get; set; }

        public async Task MigrateDbAsync()
        {
            await base.Database.MigrateAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfficeEntity>(e =>
            {
                e.Property(x => x.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                e.HasMany(x => x.Employees).WithOne().HasForeignKey(x => x.OfficeId).IsRequired();
            });

            modelBuilder.Entity<EmployeeEntity>(e =>
            {
                e.Property(x => x.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd()
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
