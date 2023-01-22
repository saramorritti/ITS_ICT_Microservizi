using System.ComponentModel.DataAnnotations;

namespace BookMicroservice.Data.Entities
{
    public class Book
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
