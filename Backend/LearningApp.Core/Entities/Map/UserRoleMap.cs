using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningApp.Core.Entities.Map
{
    public class UserRoleMap
    {
        public UserRoleMap(EntityTypeBuilder<UserRole> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId).IsRequired();
            entityBuilder.Property(t => t.RoleId).IsRequired();
        }
    }
}
