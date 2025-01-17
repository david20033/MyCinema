using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCinema.Enums;

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
        public virtual DbSet<Screening> Screening { get; set; }
        public virtual DbSet<TicketOrder> TicketOrder { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Spoken_languages)
                .WithMany(l => l.SpokenMovies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieLanguages",
                    j => j.HasOne<Language>().WithMany().HasForeignKey("LanguageId"),
                    j => j.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                    j =>
                    {
                        j.HasKey("MovieId", "LanguageId");
                    });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(m => m.Release_date)
                    .IsRequired();

                entity.Property(m => m.Runtime)
                    .IsRequired();

                entity.Property(m => m.Overview)
                    .IsRequired();

                entity.HasOne(m => m.Original_language)
                    .WithMany()
                    .HasForeignKey(m => m.Original_languageId)
                    .OnDelete(DeleteBehavior.SetNull); 
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

                entity.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.Property(l => l.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TheatreSalon>(entity =>
            {
                entity.HasKey(ts => ts.Id);

                entity.Property(ts => ts.SalonNumber)
                    .IsRequired();

                entity.Property(ts => ts.Rows)
                    .IsRequired();

                entity.Property(ts => ts.Columns)
                    .IsRequired();

                entity.Property(ts => ts.Capacity)
                    .IsRequired();

                entity.Property(ts => ts.isVip)
                    .IsRequired();
            });

            modelBuilder.Entity<Screening>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.StartTime)
                    .IsRequired();
                entity.Property(s => s.EndTime)
                    .IsRequired();

                entity.Property(s => s.Duration)
                    .IsRequired();

                entity.Property(s => s.TicketPrice)
                    .HasColumnType("decimal(18,2)") 
                    .IsRequired();

                entity.HasOne(s => s.Movie)
                    .WithMany(m => m.Screenings)
                    .HasForeignKey(s => s.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.TheatreSalon)
                    .WithMany(ts => ts.Screenings)
                    .HasForeignKey(s => s.TheatreSalonId)
                    .OnDelete(DeleteBehavior.Cascade); 
            });

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Spoken_languages)
                .WithMany(l => l.SpokenMovies)
                .UsingEntity(j => j.ToTable("MovieLanguages"));
            modelBuilder.Entity<TicketOrder>()
                .HasMany(t => t.Tickets)
                .WithOne(t => t.TicketOrder)
                .HasForeignKey(t => t.TicketOrderId)
                .OnDelete(DeleteBehavior.Cascade); 

            //modelBuilder.Entity<Ticket>()
            //    .HasDiscriminator<TicketType>("TicketType") 
            //    .HasValue<Regular>(TicketType.Regular)
            //    .HasValue<VIPTicket>(TicketType.VIP);

            modelBuilder.Entity<Ticket>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ticket>()
                .Property(t => t.SeatNumber)
                .IsRequired()
                .HasMaxLength(10); 

            modelBuilder.Entity<TicketOrder>()
                .Property(t => t.OrderDate)
                .HasDefaultValueSql("GETDATE()");
        }


    }
}
