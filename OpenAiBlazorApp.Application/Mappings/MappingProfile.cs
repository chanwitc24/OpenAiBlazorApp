using AutoMapper;
using OpenAiBlazorApp.Core.Entities;
using OpenAiBlazorApp.Core.ViewModels;

namespace OpenAiBlazorApp.Application.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserResponse>();
        CreateMap<User, UserRequest>();
        CreateMap<UserRequest, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        CreateMap<UserResponse, User>();

        CreateMap<Category, CategoryRequest>();
        CreateMap<CategoryRequest, Category>();
    }
}
