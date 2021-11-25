using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace dotnet_graphql_postgres.Models
{
    [Table("customers")]
    [Index(nameof(SupportRepId), Name = "ifk_customer_support_rep_id")]
    public partial class Customer
    {
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("first_name")]
        [StringLength(40)]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(20)]
        public string LastName { get; set; }
        [Column("company")]
        [StringLength(80)]
        public string Company { get; set; }
        [Column("address")]
        [StringLength(70)]
        public string Address { get; set; }
        [Column("city")]
        [StringLength(40)]
        public string City { get; set; }
        [Column("state")]
        [StringLength(40)]
        public string State { get; set; }
        [Column("country")]
        [StringLength(40)]
        public string Country { get; set; }
        [Column("postal_code")]
        [StringLength(10)]
        public string PostalCode { get; set; }
        [Column("phone")]
        [StringLength(24)]
        public string Phone { get; set; }
        [Column("fax")]
        [StringLength(24)]
        public string Fax { get; set; }
        [Required]
        [Column("email")]
        [StringLength(60)]
        public string Email { get; set; }
        [Column("support_rep_id")]
        public int? SupportRepId { get; set; }

        [ForeignKey(nameof(SupportRepId))]
        [InverseProperty(nameof(Employee.Customers))]
        public virtual Employee SupportRep { get; set; }
        [InverseProperty(nameof(Invoice.Customer))]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
