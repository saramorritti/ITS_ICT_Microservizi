using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Services.Interfaces
{
    public interface ICustomerServices
    {
        public Task<List<Customers>> GetAll();
        public Task<Customers> GetOne(int id);
        public Task<int> Create(CustomersBaseModel value);
        public Task Update(int id, CustomersBaseModel value);
        public void Delete(int id);

    }
}
