using Chat.Domain.Models;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Chat.Infra.Data.Configurations
{
    public class AttachmentConfiguration : BaseModelConfiguration<Attachment, int>
    {
        public AttachmentConfiguration(EntityTypeBuilder<Attachment> builder) : base(builder)
        {
            builder.HasKey(m => m.Id);

            builder.ToTable("Attachments");

            builder.HasOne(d => d.Message)
                .WithOne(p => p.Attachment)
                .HasForeignKey<Message>(d => d.AttachmentId)
                .OnDelete(DeleteBehavior.NoAction);


            //this.HasRequired(a => a.Room)
            //    .WithMany(r => r.Attachments)
            //    .HasForeignKey(a => a.RoomId);

            //this.HasRequired(a => a.Owner)
            //    .WithMany(r => r.Attachments)
            //    .HasForeignKey(a => a.OwnerId);
        }
    }
}