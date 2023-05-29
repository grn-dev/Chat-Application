using Chat.Domain.Models;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configurations
{
    public class ChatRoomConfiguration : BaseModelConfiguration<ChatRoom, int>
    {
        public ChatRoomConfiguration(EntityTypeBuilder<ChatRoom> builder) : base(builder)
        {
            //// Properties
            builder.Property(r => r.InviteCode)
                .IsFixedLength()
                .HasMaxLength(6);

            builder.Property(r => r.Topic)
                .HasMaxLength(80);

            builder
                .HasMany(e => e.ChatRoomUsers)
                .WithOne(p => p.ChatRoom)
                .HasForeignKey(p => p.ChatRoomId).OnDelete(DeleteBehavior.NoAction);


            builder
                .HasMany(e => e.Channels)
                .WithOne(p => p.ChatRoom)
                .HasForeignKey(p => p.ChatRoomId).OnDelete(DeleteBehavior.NoAction);


            builder
                .HasMany(e => e.Groups)
                .WithOne(p => p.ChatRoom)
                .HasForeignKey(p => p.ChatRoomId).OnDelete(DeleteBehavior.NoAction);


            builder.ToTable("ChatRooms");
        }
    }
}