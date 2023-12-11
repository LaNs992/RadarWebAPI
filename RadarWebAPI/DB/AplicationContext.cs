using Microsoft.EntityFrameworkCore;
using RadarWebAPI.Models;

namespace RadarWebAPI.DB
{
    public class AplicationContext:DbContext
    {
        public DbSet<Posts> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        public AplicationContext(DbContextOptions<AplicationContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Posts>().HasKey(p => p.Id);
            modelBuilder.Entity<Posts>().ToTable("DBPost");
            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().ToTable("DBUser");
        }
    }
}
  