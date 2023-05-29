using Chat.Domain.Models;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configuration
{
    public class DirectConfiguration : BaseModelConfiguration<Direct, int>
    {
        public DirectConfiguration(EntityTypeBuilder<Direct> builder) : base(builder)
        {
            builder.HasMany(d => d.DirectUsers)
                .WithOne(p => p.Direct)
                .HasForeignKey(d => d.DirectId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(d => d.DirectMessages)
                .WithOne(p => p.Direct)
                .HasForeignKey(d => d.DirectId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Direct");
        }
    }
}