using Chat.Domain.Models;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configuration
{
    public class ChatRoomMessageConfiguration : BaseModelConfiguration<ChatRoomMessage, int>
    {
        public ChatRoomMessageConfiguration(EntityTypeBuilder<ChatRoomMessage> builder) : base(builder)
        {
            builder.HasOne(d => d.ChatRoom)
                .WithMany(p => p.ChatRoomMessages)
                .HasForeignKey(d => d.ChatRoomId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Message)
                .WithMany(p => p.ChatRoomMessages)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Sender)
                .WithMany(p => p.ChatRoomMessages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.ToTable("ChatRoomMessage");
        }
    }
}