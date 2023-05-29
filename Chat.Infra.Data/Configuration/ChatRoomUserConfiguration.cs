using Chat.Domain.Models;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configuration
{
    public class ChatRoomUserConfiguration : BaseModelConfiguration<ChatRoomUser, int>
    {
        public ChatRoomUserConfiguration(EntityTypeBuilder<ChatRoomUser> builder) : base(builder)
        {
            builder.HasOne(d => d.ChatRoom)
                .WithMany(p => p.ChatRoomUsers)
                .HasForeignKey(d => d.ChatRoomId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.User)
                .WithMany(p => p.ChatRoomUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("ChatRoomUser");
        }
    }
}