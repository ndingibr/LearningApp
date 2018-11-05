using Microsoft.EntityFrameworkCore;

namespace LearningApp.Infrastructure
{
    public class LearningAppContext : DbContext
    {
        public LearningAppContext(DbContextOptions<LearningAppContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
