using EkoStatApi.Dtos;
using EkoStatApi.Models;

namespace EkoStatApi;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ArticleRequestDto, Article>();
        CreateMap<Article, ArticleResponseDto>();

        CreateMap<EntryRequestDto, Entry>();
        CreateMap<Entry, EntryResponseDto>()
            .ForMember(
                dest => dest.ArticleName,
                options => options.MapFrom(src => src.Article.Name))
            .ForMember(
                dest => dest.UnitName,
                options => options.MapFrom(src => src.Unit.Name));

        CreateMap<TagRequestDto, Tag>();
        CreateMap<Tag, TagResponseDto>();

        CreateMap<UnitRequestDto, Unit>();
        CreateMap<Unit, UnitResponseDto>();

        CreateMap<UserRequestDto, User>();
        CreateMap<User, UserResponseDto>();
    }
}
