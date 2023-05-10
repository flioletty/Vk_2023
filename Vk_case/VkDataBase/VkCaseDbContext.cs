using Microsoft.EntityFrameworkCore;
using VkDataBase.Models;

namespace VkDataBase
{
    public class VkCaseDbContext : DbContext
    {
        public VkCaseDbContext(DbContextOptions<VkCaseDbContext> options) : base(options) { Database.EnsureCreated(); }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<UserState> UserState { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.CreatedDate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<UserState>().Property(u => u.Code).HasDefaultValue(UserStateCode.Active);
            modelBuilder.Entity<UserGroup>().Property(u => u.Code).HasDefaultValue(UserGroupCode.User);
        }
    }
}
