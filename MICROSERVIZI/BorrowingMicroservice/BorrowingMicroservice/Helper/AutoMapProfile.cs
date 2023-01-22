using AutoMapper;
using BorrowingMicroservice.Data.Entities;
using BorrowingMicroservice.Models;

namespace BorrowingMicroservice.Helper
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            CreateMap<Borrowings, Borrowing>().ReverseMap();
            CreateMap<BorrowingsBaseModel, Borrowing>().ReverseMap();
        }
    }
}
