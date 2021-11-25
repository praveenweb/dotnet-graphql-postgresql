using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace dotnet_graphql_postgres.Models
{
    [Table("tracks")]
    [Index(nameof(AlbumId), Name = "ifk_track_album_id")]
    [Index(nameof(GenreId), Name = "ifk_track_genre_id")]
    [Index(nameof(MediaTypeId), Name = "ifk_track_media_type_id")]
    public partial class Track
    {
        public Track()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
            PlaylistTracks = new HashSet<PlaylistTrack>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(200)]
        public string Name { get; set; }
        [Column("album_id")]
        public int? AlbumId { get; set; }
        [Column("media_type_id")]
        public int MediaTypeId { get; set; }
        [Column("genre_id")]
        public int? GenreId { get; set; }
        [Column("composer")]
        [StringLength(220)]
        public string Composer { get; set; }
        [Column("milliseconds")]
        public int Milliseconds { get; set; }
        [Column("bytes")]
        public int? Bytes { get; set; }
        [Column("unit_price")]
        public decimal UnitPrice { get; set; }

        [ForeignKey(nameof(AlbumId))]
        [InverseProperty("Tracks")]
        public virtual Album Album { get; set; }
        [ForeignKey(nameof(GenreId))]
        [InverseProperty("Tracks")]
        public virtual Genre Genre { get; set; }
        [ForeignKey(nameof(MediaTypeId))]
        [InverseProperty("Tracks")]
        public virtual MediaType MediaType { get; set; }
        [InverseProperty(nameof(InvoiceLine.Track))]
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
        [InverseProperty(nameof(PlaylistTrack.Track))]
        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }
}
