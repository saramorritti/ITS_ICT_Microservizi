using BookMicroservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMicroservice.Services.Interfaces
{
    public interface IBookServices
    {
        public Task<List<Books>> GetAll();
        public Task<Books> GetOne(int id);
        public Task<int> Create(BooksBaseModel value);
        public Task Update(int id, BooksBaseModel value);
        public void Delete(int id);
    }
}
