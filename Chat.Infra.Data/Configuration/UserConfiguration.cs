using Chat.Domain.Models.User;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configuration
{
    public class UserConfiguration : BaseModelConfiguration<User, int>
    {
        public UserConfiguration(EntityTypeBuilder<User> builder) : base(builder)
        {
 

            builder.Property(u => u.UserName)
                .HasMaxLength(255);

            builder.Property(u => u.FirstName)
                .HasMaxLength(255);

            builder.Property(u => u.LastName)
                .HasMaxLength(255);

            builder.Property(u => u.UserName)
                .HasMaxLength(255);

            builder.Property(u => u.NationalCode)
                .HasMaxLength(10);

            builder.Property(u => u.MobileNumber)
                .HasMaxLength(11);
            
            

            // Table & Column Mappings
            builder.ToTable("Users");
        }
    }
}