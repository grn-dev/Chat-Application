using Chat.Domain.Models;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configuration
{
    public class DirectUserConfiguration : BaseModelConfiguration<DirectUser, int>
    {
        public DirectUserConfiguration(EntityTypeBuilder<DirectUser> builder) : base(builder)
        {
            builder.HasOne(d => d.User)
                .WithMany(p => p.DirectUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Direct)
                .WithMany(p => p.DirectUsers)
                .HasForeignKey(d => d.DirectId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("DirectUser");
        }
    }
}