using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CustomerMicroservice.Models
{
    public partial class Customers
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }
        [Required]
        [StringLength(50)]
        public string Mail { get; set; }
        [Required]
        [StringLength(10)]
        public string Telefono { get; set; }
    }
}
