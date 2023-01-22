using AutoMapper;
using BorrowingMicroservice.Data;
using BorrowingMicroservice.Data.Entities;
using BorrowingMicroservice.Models;
using BorrowingMicroservice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorrowingMicroservice.Services
{
    public class BorrowingServices : IBorrowingServices
    {

        private readonly BorrowingContext _context;
        private readonly IMapper _mapper;
        private static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public BorrowingServices(BorrowingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Create(BorrowingsBaseModel value)
        {
            var entity = _mapper.Map<Borrowing>(value);
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            
            _context.Borrowings.Add(entity);
            await _context.SaveChangesAsync();

            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;

            Log.Info("BorrowingMicroservice:[Create] Time required to create a record: " + _systime + "ms");
            return entity.Id;
        }



        public async Task<List<Borrowings>> GetAll()
        {
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            try
            {
                var list = await _context.Borrowings.ToListAsync();

                var listBorrowings = new List<Borrowings>();

                foreach (var boo in list)
                {

                    listBorrowings.Add(_mapper.Map<Borrowings>(boo));
                }
                Log.Info("BorrowingMicroservice:[GetAll] Time required to create a record: " + _systime + "ms");

                return listBorrowings;
            }
            catch (Exception e)
            {
                Log.Error("BorrowingMicroservice:[GetAll] Book does not exist");

                throw e;
            }
        }

        public async Task<Borrowings> GetOne(int id)
        {
            var item = await _context.Borrowings.FirstOrDefaultAsync(x => x.Id == id);
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            Log.Info("BorrowingMicroservice:[GetOne] Time required to create a record: " + _systime + "ms");
            return _mapper.Map<Borrowings>(item);
        }

        public async Task Update(int id, BorrowingsBaseModel value)
        {
            var itf = _context.Borrowings.FirstOrDefault(t => t.Id == id);
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            if (itf == null)
                throw new System.Exception("Item Not Found");

            itf.CustomerId = value.CustomerId;
            itf.Date = value.Date;

            Log.Info("BorrowingMicroservice:[Update] Time required to create a record: " + _systime + "ms");

            _context.Update(itf);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var item = _context.Borrowings.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                Log.Error("BorrowingMicroservice:[Delete] Book does not exist");
                throw new System.Exception("Item not found");


            }
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            Log.Info("CustomerMicroservice:[Delete] Time required to delete a record: " + _systime + "ms");
            _context.Borrowings.Remove(item);
            _context.SaveChanges();
        }



    }

}


