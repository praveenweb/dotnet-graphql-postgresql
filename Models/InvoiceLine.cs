using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace dotnet_graphql_postgres.Models
{
    [Table("invoice_lines")]
    [Index(nameof(InvoiceId), Name = "ifk_invoice_line_invoice_id")]
    [Index(nameof(TrackId), Name = "ifk_invoice_line_track_id")]
    public partial class InvoiceLine
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("invoice_id")]
        public int InvoiceId { get; set; }
        [Column("track_id")]
        public int TrackId { get; set; }
        [Column("unit_price")]
        public decimal UnitPrice { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        [InverseProperty("InvoiceLines")]
        public virtual Invoice Invoice { get; set; }
        [ForeignKey(nameof(TrackId))]
        [InverseProperty("InvoiceLines")]
        public virtual Track Track { get; set; }
    }
}
