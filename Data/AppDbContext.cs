using SoccerHub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace SoccerHub.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Team>().
                HasOne(x => x.User)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            /*var admin = new User
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@gmail.com",

                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),

                Role = UserRole.Admin
            };

            modelBuilder.Entity<User>()
                .HasData(admin);*/

            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}