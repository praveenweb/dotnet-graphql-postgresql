using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace dotnet_graphql_postgres.Models
{
    [Table("employees")]
    [Index(nameof(ReportsTo), Name = "ifk_employee_reports_to")]
    public partial class Employee
    {
        public Employee()
        {
            Customers = new HashSet<Customer>();
            InverseReportsToNavigation = new HashSet<Employee>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [Column("first_name")]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Column("title")]
        [StringLength(30)]
        public string Title { get; set; }
        [Column("reports_to")]
        public int? ReportsTo { get; set; }
        [Column("birth_date")]
        public DateTime? BirthDate { get; set; }
        [Column("hire_date")]
        public DateTime? HireDate { get; set; }
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
        [Column("email")]
        [StringLength(60)]
        public string Email { get; set; }

        [ForeignKey(nameof(ReportsTo))]
        [InverseProperty(nameof(Employee.InverseReportsToNavigation))]
        public virtual Employee ReportsToNavigation { get; set; }
        [InverseProperty(nameof(Customer.SupportRep))]
        public virtual ICollection<Customer> Customers { get; set; }
        [InverseProperty(nameof(Employee.ReportsToNavigation))]
        public virtual ICollection<Employee> InverseReportsToNavigation { get; set; }
    }
}
