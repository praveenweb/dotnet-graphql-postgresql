using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace dotnet_graphql_postgres.Models
{
    [Table("film_actor")]
    [Index(nameof(FilmId), Name = "idx_fk_films_id")]
    public partial class FilmActor
    {
        [Key]
        [Column("actor_id")]
        public short ActorId { get; set; }
        [Key]
        [Column("film_id")]
        public short FilmId { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
