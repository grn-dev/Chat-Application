using Chat.Domain.Models;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configurations
{
    public class MessageConfiguration : BaseModelConfiguration<Message, int>

    {
        public MessageConfiguration(EntityTypeBuilder<Message> builder) : base(builder)
        {  // Primary Key
            builder.HasKey(m => m.Id);

            // Properties
            // Table & Column Mappings
            builder.ToTable("Messages");

            // Relationships
            
            builder.HasOne(d => d.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.NoAction) ;

            builder.HasOne(d => d.User)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction) ;
            
            
            //this.HasOptional(m => m.Room)
            //    .WithMany(r => r.Messages)
            //    .HasForeignKey(m => m.RoomId);

            //this.HasOptional(m => m.User)
            //    .WithMany()
            //    .HasForeignKey(m => m.UserKey);
        }
    }
}