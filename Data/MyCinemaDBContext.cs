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
        public virtual DbSet<MoviePhoto> MoviePhoto { get; set; }
        public virtual DbSet<TheatreSalon> TheatreSalon { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Title).IsRequired().HasMaxLength(200);
                entity.Property(m => m.Release_date).IsRequired();
                entity.Property(m => m.Runtime).IsRequired();
                entity.Property(m => m.Overview).IsRequired();
            });

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Name).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<MovieActor>(entity =>
            {
                entity.HasKey(ma => new { ma.MovieId, ma.ActorId });

                entity.HasOne(ma => ma.Movie)
                      .WithMany(m => m.MovieActors)
                      .HasForeignKey(ma => ma.MovieId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ma => ma.Actor)
                      .WithMany(a => a.MovieActors)
                      .HasForeignKey(ma => ma.ActorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MoviePhoto>(entity =>
            {
                entity.HasKey(mp => mp.Id);

                entity.HasOne(mp => mp.Movie)
                      .WithMany(m => m.MoviePhotos)
                      .HasForeignKey(mp => mp.MovieId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.HasKey(mg => new { mg.MovieId, mg.GenreId });

                entity.HasOne(mg => mg.Movie)
                      .WithMany(m => m.Genres)
                      .HasForeignKey(mg => mg.MovieId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(mg => mg.Genre)
                      .WithMany(g => g.MovieGenres)
                      .HasForeignKey(mg => mg.GenreId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(g => g.Id);

                entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
            });
            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.Property(l => l.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Spoken_languages)
                .WithMany(l => l.SpokenMovies)
                .UsingEntity(j => j.ToTable("MovieLanguages"));

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.HasOne(m => m.Original_language)
                      .WithMany() 
                      .HasForeignKey(m => m.Original_languageId) 
                      .OnDelete(DeleteBehavior.SetNull); 
            });
        }

    }
}
