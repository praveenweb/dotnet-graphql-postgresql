using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace dotnet_graphql_postgres.Models
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<FilmActor> FilmActors { get; set; }
        public virtual DbSet<FilmCategory> FilmCategories { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<PlaylistTrack> PlaylistTracks { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                var databaseInfo = Environment.GetEnvironmentVariable("DATABASE_STRING");
                var defaultInfo = "Host=localhost;Database=postgres;Port=5432;Username=postgres;Password=postgrespassword";
                optionsBuilder.UseNpgsql(databaseInfo ?? defaultInfo);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pgcrypto")
                .HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.LastUpdate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_album_artist_id");
            });


            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.LastUpdate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasOne(d => d.SupportRep)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.SupportRepId)
                    .HasConstraintName("fk_customer_support_rep_id");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("fk_employee_reports_to");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasIndex(e => e.Fulltext, "films_fulltext_idx")
                    .HasMethod("gist");

                entity.Property(e => e.LastUpdate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Rating).HasDefaultValueSql("'G'::text");

                entity.Property(e => e.RentalDuration).HasDefaultValueSql("3");

                entity.Property(e => e.RentalRate)
                    .HasPrecision(4, 2)
                    .HasDefaultValueSql("4.99");

                entity.Property(e => e.ReplacementCost)
                    .HasPrecision(5, 2)
                    .HasDefaultValueSql("19.99");
            });

            modelBuilder.Entity<FilmActor>(entity =>
            {
                entity.HasKey(e => new { e.ActorId, e.FilmId })
                    .HasName("film_actor_pkey");

                entity.Property(e => e.LastUpdate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<FilmCategory>(entity =>
            {
                entity.Property(e => e.LastUpdate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Total).HasPrecision(10, 2);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_invoice_customer_id");
            });

            modelBuilder.Entity<InvoiceLine>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('invoicelines_id_seq'::regclass)");

                entity.Property(e => e.UnitPrice).HasPrecision(10, 2);

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceLines)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_invoice_line_invoice_id");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.InvoiceLines)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_invoice_line_track_id");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('mediatypes_id_seq'::regclass)");
            });

            modelBuilder.Entity<PlaylistTrack>(entity =>
            {
                entity.HasKey(e => new { e.PlaylistId, e.TrackId })
                    .HasName("pk_playlist_track");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.PlaylistTracks)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_playlist_track_playlist_id");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.PlaylistTracks)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_playlist_track_track_id");
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.Property(e => e.UnitPrice).HasPrecision(10, 2);

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.AlbumId)
                    .HasConstraintName("fk_track_album_id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("fk_track_genre_id");

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.MediaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_track_media_type_id");
            });

            modelBuilder.HasSequence("actors_id_seq")
                .StartsAt(203)
                .HasMax(2147483647);

            modelBuilder.HasSequence("albums_id_seq")
                .StartsAt(347)
                .HasMax(2147483647);

            modelBuilder.HasSequence("artists_id_seq")
                .StartsAt(275)
                .HasMax(2147483647);

            modelBuilder.HasSequence("categories_id_seq")
                .StartsAt(16)
                .HasMax(2147483647);

            modelBuilder.HasSequence("customers_id_seq")
                .StartsAt(59)
                .HasMax(2147483647);

            modelBuilder.HasSequence("employees_id_seq")
                .StartsAt(8)
                .HasMax(2147483647);

            modelBuilder.HasSequence("films_id_seq")
                .StartsAt(1000)
                .HasMax(2147483647);

            modelBuilder.HasSequence("genres_id_seq")
                .StartsAt(25)
                .HasMax(2147483647);

            modelBuilder.HasSequence("invoicelines_id_seq")
                .StartsAt(2240)
                .HasMax(2147483647);

            modelBuilder.HasSequence("invoices_id_seq")
                .StartsAt(412)
                .HasMax(2147483647);

            modelBuilder.HasSequence("mediatypes_id_seq")
                .StartsAt(5)
                .HasMax(2147483647);

            modelBuilder.HasSequence("playlists_id_seq")
                .StartsAt(18)
                .HasMax(2147483647);

            modelBuilder.HasSequence("tracks_id_seq")
                .StartsAt(3503)
                .HasMax(2147483647);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
