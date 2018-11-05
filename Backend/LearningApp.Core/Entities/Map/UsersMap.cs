using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningApp.Core.Entities.Map
{
    public class UsersMap
    {
        public UsersMap(EntityTypeBuilder<Users> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);

        }
    }
}
