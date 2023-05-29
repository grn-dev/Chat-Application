using Chat.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Chat.Infra.Data.Configurations.BaseModelConfigurations
{
    public class BaseModelConfiguration<TEntity, TPrimaryKey> where TEntity : BaseModel<TPrimaryKey>
    {
        public BaseModelConfiguration(EntityTypeBuilder<TEntity> builder)
        {
            //PK
            builder.HasKey(t => t.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();


            //CreateUserId
            builder
                .Property(c => c.CreatorUserId)
                .IsRequired();


            //CreateDate
            builder
              .Property(m => m.CreateDate)
              .IsRequired()
              .HasDefaultValueSql("getDate()");


            //CreatorUserId
            builder
              .Property(m => m.CreatorUserId)
              .IsRequired();


            builder.ToTable(typeof(TEntity).Name);
        }
    }
}
