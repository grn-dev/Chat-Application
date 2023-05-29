using Chat.Domain.Models.Ticket;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configuration
{
    public class TicketConfiguration : BaseModelConfiguration<Ticket, int>
    {
        public TicketConfiguration(EntityTypeBuilder<Ticket> builder) : base(builder)
        {


            builder.Property(t => t.Subject).HasMaxLength(255); 
            builder
                     .ToTable("Ticket");


        }

    }
}



