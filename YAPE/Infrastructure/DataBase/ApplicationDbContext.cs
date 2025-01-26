using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<YapeValidatedClient> YapeValidatedClients { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<YapeValidatedClient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.CellPhoneNumber).IsRequired();
                entity.Property(e => e.DocumentType).IsRequired();
                entity.Property(e => e.DocumentNumber).IsRequired();
                entity.Property(e => e.ReasonOfUse).IsRequired();
            });
        }
    }
}
