using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace dotnet_graphql_postgres.Models
{
    [Table("playlist_track")]
    [Index(nameof(TrackId), Name = "ifk_playlist_track_track_id")]
    public partial class PlaylistTrack
    {
        [Key]
        [Column("playlist_id")]
        public int PlaylistId { get; set; }
        [Key]
        [Column("track_id")]
        public int TrackId { get; set; }

        [ForeignKey(nameof(PlaylistId))]
        [InverseProperty("PlaylistTracks")]
        public virtual Playlist Playlist { get; set; }
        [ForeignKey(nameof(TrackId))]
        [InverseProperty("PlaylistTracks")]
        public virtual Track Track { get; set; }
    }
}
