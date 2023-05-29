using Chat.Domain.Models;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configuration
{
    public class DirectMessageConfiguration : BaseModelConfiguration<DirectMessage, int>
    {
        public DirectMessageConfiguration(EntityTypeBuilder<DirectMessage> builder) : base(builder)
        {
            builder.HasOne(d => d.Direct)
                .WithMany(p => p.DirectMessages)
                .HasForeignKey(d => d.DirectId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Message)
                .WithMany(p => p.DirectMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Sender)
                .WithMany(p => p.DirectMessages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.ToTable("DirectMessages");
        }
    }
}