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
#if DEBUG
        private static readonly string connectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=DefaultConnection;Integrated Security=True";
#else
        private static readonly string connectionString = @"Server=tcp:m8a4ohtorq.database.windows.net,1433;Database=mundialAGEG1NQnv;User ID=ezraroi@m8a4ohtorq;Password=RoiDror123;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
#endif

        public DbSet<Game> Games { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Bet> Bets { get; set; }

        public MundialitoContext()
            : base("name=TestConnection")
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