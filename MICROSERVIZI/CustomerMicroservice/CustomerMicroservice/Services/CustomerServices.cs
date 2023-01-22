using AutoMapper;
using CustomerMicroservice.Data;
using CustomerMicroservice.Data.Entities;
using CustomerMicroservice.Models;
using CustomerMicroservice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Services
{
    public class CustomerServices : ICustomerServices
    {

        private readonly CustomerContext _context;
        private readonly IMapper _mapper;
        private static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CustomerServices(CustomerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Create(CustomersBaseModel value)
        {
            var entity = _mapper.Map<Customer>(value);
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();

            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;

            Log.Info("CustomerMicroservice:[Create] Time required to create a record: " + _systime + "ms");
            return entity.Id;
        }



        public async Task<List<Customers>> GetAll()
        {
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            try
            {
                var list = await _context.Customers.ToListAsync();

                var listCustomers = new List<Customers>();

                foreach (var boo in list)
                {

                    listCustomers.Add(_mapper.Map<Customers>(boo));
                }

                Log.Info("CustomerMicroservice:[GetAll] Time required to create a record: " + _systime + "ms");
                return listCustomers;
            }
            catch (Exception e)
            {

                Log.Error("CustomerMicroservice:[GetAll] Customer does not exist");
                throw e;
            }
        }

        public async Task<Customers> GetOne(int id)
        {
            var item = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            Log.Info("CustomerMicroservice:[GetOne] Time required to create a record: " + _systime + "ms");
            return _mapper.Map<Customers>(item);
        }

        public async Task Update(int id, CustomersBaseModel value)
        {
            var itf = _context.Customers.FirstOrDefault(t => t.Id == id);
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            if (itf == null)
                throw new System.Exception("Item Not Found");

            itf.Id = value.Id;
            itf.Nome = value.Nome;
            itf.Cognome = value.Cognome;

            Log.Info("CustomerMicroservice:[Update] Time required to create a record: " + _systime + "ms");
            _context.Update(itf);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var item = _context.Customers.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                Log.Error("CustomerMicroservice:[Delete] Customer does not exist");
                throw new System.Exception("Item not found");


            }
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            Log.Info("CustomerMicroservice:[Delete] Time required to delete a record: " + _systime + "ms");
            _context.Customers.Remove(item);
            _context.SaveChanges();
        }



    }
}
