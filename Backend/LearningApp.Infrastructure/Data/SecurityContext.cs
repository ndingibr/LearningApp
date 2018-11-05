using LearningApp.Core.Entities;
using LearningApp.Core.Entities.Map;
using Microsoft.EntityFrameworkCore;


namespace LearningApp.Infrastructure
{
    public class SecurityContext : DbContext
    {
        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UsersMap(modelBuilder.Entity<Users>());
            new RolesMap(modelBuilder.Entity<Roles>());
            new UserRoleMap(modelBuilder.Entity<UserRole>());

        }
    }
}
