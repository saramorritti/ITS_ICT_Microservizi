using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BorrowingMicroservice.Models
{
    public class BorrowingsBaseModel
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}
// id customerid bookid datetime