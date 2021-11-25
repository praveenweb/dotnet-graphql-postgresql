using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace dotnet_graphql_postgres.Models
{
    [Keyless]
    [Table("film_category")]
    public partial class FilmCategory
    {
        [Column("film_id")]
        public short FilmId { get; set; }
        [Column("category_id")]
        public short CategoryId { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
