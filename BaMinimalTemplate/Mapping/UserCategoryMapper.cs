using AutoMapper;
using BaMinimalTemplate.Dtos.UserCategories;
using BaMinimalTemplate.Models;

namespace BaMinimalTemplate.Mapping;

public class UserCategoryMapper : Profile
{
    public UserCategoryMapper()
    {
        CreateMap<UserCategory, UserCategoryDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

        CreateMap<UserCategory, UserCategoryListDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

        CreateMap<UserCategoryCreateDto, UserCategory>();
        CreateMap<UserCategoryUpdateDto, UserCategory>();
    }
}
