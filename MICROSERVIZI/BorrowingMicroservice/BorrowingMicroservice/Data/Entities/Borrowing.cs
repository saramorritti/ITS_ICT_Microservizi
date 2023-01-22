using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BorrowingMicroservice.Data.Entities
{
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}
