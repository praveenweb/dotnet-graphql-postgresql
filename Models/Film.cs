using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

#nullable disable

namespace dotnet_graphql_postgres.Models
{
    [Table("films")]
    [Index(nameof(LanguageId), Name = "idx_fk_language_id")]
    [Index(nameof(Title), Name = "idx_title")]
    public partial class Film
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("release_year")]
        public int? ReleaseYear { get; set; }
        [Column("language_id")]
        public short LanguageId { get; set; }
        [Column("rental_duration")]
        public short RentalDuration { get; set; }
        [Column("rental_rate")]
        public decimal RentalRate { get; set; }
        [Column("length")]
        public short? Length { get; set; }
        [Column("replacement_cost")]
        public decimal ReplacementCost { get; set; }
        [Column("rating")]
        public string Rating { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
        [Column("special_features")]
        public string[] SpecialFeatures { get; set; }
        [Required]
        [Column("fulltext")]
        public NpgsqlTsVector Fulltext { get; set; }
    }
}
