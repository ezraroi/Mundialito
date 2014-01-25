using System.Data.Entity;
using Mundialito.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.Games;
using Mundialito.DAL.Teams;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Bets;

namespace Mundialito.DAL
{
    public class MundialitoContext : IdentityDbContext<MundialitoUser> 
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Bet> Bets { get; set; }

        public MundialitoContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>().HasRequired(x => x.HomeTeam) //or HasOptional
                                       .WithMany() //Unidirectional
                                       .Map(x => x.MapKey("HomeTeam")) //FK column Name
                                       .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>().HasRequired(x => x.AwayTeam) //or HasOptional
                                       .WithMany() //Unidirectional
                                       .Map(x => x.MapKey("AwayTeam")) //FK column Name
                                       .WillCascadeOnDelete(false);

            /*
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<MundialitoUser>()
                .ToTable("Users");
            */

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}