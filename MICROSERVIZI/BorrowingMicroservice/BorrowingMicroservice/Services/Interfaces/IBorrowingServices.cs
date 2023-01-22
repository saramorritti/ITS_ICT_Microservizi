using BorrowingMicroservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BorrowingMicroservice.Services.Interfaces
{
    public interface IBorrowingServices
    {
        public Task<List<Borrowings>> GetAll();
        public Task<Borrowings> GetOne(int id);
        public Task<int> Create(BorrowingsBaseModel value);
        public Task Update(int id, BorrowingsBaseModel value);
        public void Delete(int id);

    }
}