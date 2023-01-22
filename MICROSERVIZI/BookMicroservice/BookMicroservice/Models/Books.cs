using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMicroservice.Models
{
    public class Books
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Titolo { get; set; }
        [Required]
        [StringLength(200)]
        public string Autore { get; set; }
        public int? Anno { get; set; }
        public bool Disponibile { get; set; }
    }
}
