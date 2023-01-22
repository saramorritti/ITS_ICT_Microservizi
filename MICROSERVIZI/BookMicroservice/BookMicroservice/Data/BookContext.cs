using BookMicroservice.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMicroservice.Data
{
    public class BookContext : DbContext

    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        { }

        public DbSet<Book> Books => Set<Book>();
    }
}