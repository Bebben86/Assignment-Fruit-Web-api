using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndreasFruit_api.Models;
using AutoMapper;

namespace AndreasFruit_api.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Category, ViewModels.Category.ViewModel>();
            //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<ViewModels.Category.PostViewModel, Category>();
        }
    }
}