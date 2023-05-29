using Chat.Language.Model;
using Microsoft.EntityFrameworkCore;

namespace Chat.Language
{
    public class LocalizationDbContext : DbContext
    {
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MakaLang");
        }
    }
}