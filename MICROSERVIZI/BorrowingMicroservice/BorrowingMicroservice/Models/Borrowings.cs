using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BorrowingMicroservice.Models
{
    public partial class Borrowings
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}
// id customerid bookid date