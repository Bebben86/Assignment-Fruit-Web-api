using AndreasFruit_api.Models;
using AutoMapper;

namespace AndreasFruit_api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //FRÅN, TILL
            CreateMap<Category, ViewModels.Category.ViewModel>();
            CreateMap<ViewModels.Category.PostViewModel, Category>();

            CreateMap<ViewModels.Fruit.PostViewModel, Fruit>()
            //TILL, FRÅN
                .ForMember(dest => dest.Category, opt => opt.MapFrom<AddCategoryResolver>()
            );






            CreateMap<Fruit, ViewModels.Fruit.ViewModel>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
        }
    }
}