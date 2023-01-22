using AutoMapper;
using BookMicroservice.Data.Entities;
using BookMicroservice.Models;

namespace BookMicroservice.Helper
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            CreateMap<Books, Book>().ReverseMap();
            CreateMap<BooksBaseModel, Book>().ReverseMap();
        }
    }
}
