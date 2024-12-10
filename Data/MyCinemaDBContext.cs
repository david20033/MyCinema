using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyCinema.Data
{
    public class MyCinemaDBContext : IdentityDbContext<IdentityUser>
    {
        public MyCinemaDBContext(DbContextOptions<MyCinemaDBContext> options) : base(options)
        {
        }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<MovieGenre> MovieGenre { get; set; }
        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<MoviePhoto> MoviePhoto { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>(e =>
            {
                e.HasKey(m => m.Id);
                e.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(200);

                e.Property(m => m.OriginalTitle)
                .IsRequired()
                .HasMaxLength(200);

                e.Property(m => m.PremierDate)
                .IsRequired();
                e.Property(m => m.DurationInMinutes)
                .IsRequired();
                e.Property(m => m.Description)
                .IsRequired();
                e.Property(m => m.Subtitles)
                .HasDefaultValue(false);
            });


            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(mg => mg.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            modelBuilder.Entity<MoviePhoto>()
                .HasOne(p=>p.Movie)
                .WithMany(m=>m.MoviePhotos)
                .HasForeignKey(m=>m.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
