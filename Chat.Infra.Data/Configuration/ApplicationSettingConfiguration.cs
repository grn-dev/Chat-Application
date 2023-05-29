using Chat.Domain.Models;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Data.Configuration
{
    public class ApplicationSettingConfiguration : BaseModelConfiguration<ApplicationSetting, int>
    {
        public ApplicationSettingConfiguration(EntityTypeBuilder<ApplicationSetting> builder) : base(builder)
        {


            builder.Property(t => t.AzureblobStorageConnectionString).HasMaxLength(500);
            builder.Property(t => t.LocalFileSystemStoragePath).HasMaxLength(500);
            builder.Property(t => t.LocalFileSystemStorageUriPrefix).HasMaxLength(500);

            builder.ToTable("ApplicationSetting");

            #region Seed
            builder
                .HasData(
                ApplicationSetting.Seed(1, "", "", "", 5242880, 20000, StoreType.LOCALFILESTORE));
            #endregion

        }

    }
}



