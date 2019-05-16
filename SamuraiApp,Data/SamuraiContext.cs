using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamuraiApp.Domain;
using System.Configuration;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        //public SamuraiContext(DbContextOptions<SamuraiContext> options) : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connect = ConfigurationManager.ConnectionStrings["Database"].ToString();
            optionsBuilder.UseSqlServer("Server=localhost,1439;Initial Catalog=SamuraiDB;Integrated Security=false; User Id=sa;Password=Qqwerty75");
        }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(s => new { s.SamuraiId, s.BattleId });
        }
    }
}
