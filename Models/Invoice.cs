using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace dotnet_graphql_postgres.Models
{
    [Table("invoices")]
    [Index(nameof(CustomerId), Name = "ifk_invoice_customer_id")]
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("invoice_date")]
        public DateTime InvoiceDate { get; set; }
        [Column("billing_address")]
        [StringLength(70)]
        public string BillingAddress { get; set; }
        [Column("billing_city")]
        [StringLength(40)]
        public string BillingCity { get; set; }
        [Column("billing_state")]
        [StringLength(40)]
        public string BillingState { get; set; }
        [Column("billing_country")]
        [StringLength(40)]
        public string BillingCountry { get; set; }
        [Column("billing_postal_code")]
        [StringLength(10)]
        public string BillingPostalCode { get; set; }
        [Column("total")]
        public decimal Total { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Invoices")]
        public virtual Customer Customer { get; set; }
        [InverseProperty(nameof(InvoiceLine.Invoice))]
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}
