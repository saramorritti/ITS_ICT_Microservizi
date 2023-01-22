using BorrowingMicroservice.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BorrowingMicroservice.Data
{
    public class BorrowingContext : DbContext

    {
        public BorrowingContext(DbContextOptions<BorrowingContext> options) : base(options)
        { }

        public DbSet<Borrowing> Borrowings => Set<Borrowing>();
    }
}