using AutoMapper;
using BookMicroservice.Data;
using BookMicroservice.Data.Entities;
using BookMicroservice.Models;
using BookMicroservice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMicroservice.Services
{
    public class BookServices : IBookServices
    {

        private readonly BookContext _context;
        private readonly IMapper _mapper;
        private static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BookServices(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Create(BooksBaseModel value)
        {
            var entity = _mapper.Map<Book>(value);
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            _context.Books.Add(entity);
            await _context.SaveChangesAsync();

            Log.Info("BookMicroservice:[Create] Time required to create a record: " + _systime + "ms");
            return entity.Id;
        }



        public async Task<List<Books>> GetAll()
        {
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            try
            {
                var list = await _context.Books.ToListAsync();

                var listBooks = new List<Books>();

                foreach (var boo in list)
                {

                    listBooks.Add(_mapper.Map<Books>(boo));
                }

                Log.Info("BookMicroservice:[GetAll] Time required to create a record: " + _systime + "ms");
                return listBooks;
            }
            catch (Exception e)
            {

                Log.Error("BookMicroservice:[GetAll] Book does not exist");
                throw e;
            }
        }

        public async Task<Books> GetOne(int id)
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            Log.Info("BookMicroservice:[GetOne] Time required to create a record: " + _systime + "ms");
            return _mapper.Map<Books>(item);
        }

        public async Task Update(int id, BooksBaseModel value)
        {
            var itf = _context.Books.FirstOrDefault(t => t.Id == id);
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            if (itf == null)
                throw new System.Exception("Item Not Found");

            itf.Titolo = value.Titolo;
            itf.Autore = value.Autore;

            Log.Info("BookMicroservice:[Update] Time required to create a record: " + _systime + "ms");

            _context.Update(itf);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var item = _context.Books.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                Log.Error("BookMicroservice:[Delete] Book does not exist");
                throw new System.Exception("Item not found");


            }
            var _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _context.Books.Remove(item);
            _context.SaveChanges();
            _systime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _systime;
            Log.Info("BookMicroservice:[Delete] Time required to remove a record: " + _systime + "ms");
        }

    }

}


