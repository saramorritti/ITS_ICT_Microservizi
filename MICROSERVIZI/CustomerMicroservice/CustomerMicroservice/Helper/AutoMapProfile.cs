using AutoMapper;
using CustomerMicroservice.Data.Entities;
using CustomerMicroservice.Models;

namespace CustomerMicroservice.Helper
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            CreateMap<Customers, Customer>().ReverseMap();
            CreateMap<CustomersBaseModel, Customer>().ReverseMap();
        }
    }
}
