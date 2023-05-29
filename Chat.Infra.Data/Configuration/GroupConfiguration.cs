using Chat.Domain.Models;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configuration
{
    public class GroupConfiguration : BaseModelConfiguration<Group, int>
    {
        public GroupConfiguration(EntityTypeBuilder<Group> builder) : base(builder)
        { 
            builder.ToTable("Group");
        }
    }
}