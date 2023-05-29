using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configuration
{
    public class ChannelConfiguration : BaseModelConfiguration<Chat.Domain.Models.Channel, int>
    {
        public ChannelConfiguration(EntityTypeBuilder<Chat.Domain.Models.Channel> builder) : base(builder)
        { 
            builder.ToTable("Channel");
        }
    }
}