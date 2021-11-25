using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace dotnet_graphql_postgres.Models
{
    [Table("media_types")]
    public partial class MediaType
    {
        public MediaType()
        {
            Tracks = new HashSet<Track>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(120)]
        public string Name { get; set; }

        [InverseProperty(nameof(Track.MediaType))]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
