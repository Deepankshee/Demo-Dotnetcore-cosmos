using AutoMapper;
using Demo.Common.DataModels;
using Demo.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Business.AutoMapper
{
    public class MapperProfile : Profile
    {
       public MapperProfile()
        {
            CreateMap<User, AuthUserOutputModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
