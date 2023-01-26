using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicSpot_v3.Infrastructure.Data.Identity;
using MusicSpot_v3.Infrastructure.Data.Models;

namespace MusicSpot_v3.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; init; }

        public DbSet<Album> Albums { get; init; }

        public DbSet<Track> Tracks { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Artist>()
                   .HasOne(a => a.User)
                   .WithMany(a => a.Artists)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Album>()
                   .HasOne(a => a.Artist)
                   .WithMany(a => a.Albums)
                   .HasForeignKey(a => a.ArtistId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Track>()
                   .HasOne(t => t.Album)
                   .WithMany(t => t.Tracks)
                   .HasForeignKey(t => t.AlbumId)
                   .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}