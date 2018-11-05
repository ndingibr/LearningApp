using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningApp.Core.Entities.Map
{
    public class RolesMap
    {
        public RolesMap(EntityTypeBuilder<Roles> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Id).IsRequired();

        }
    }
}
