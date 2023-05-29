using Chat.Domain.Models.Ticket;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configuration
{
    public class TicketCategoryConfiguration : BaseModelConfiguration<TicketCategory, int>
    {
        public TicketCategoryConfiguration(EntityTypeBuilder<TicketCategory> builder) : base(builder)
        {


            builder.Property(t => t.Name).HasMaxLength(255);
            builder.Property(t => t.Description).HasMaxLength(500);
            builder
                     .ToTable("TicketCategory");


        }

    }
}



