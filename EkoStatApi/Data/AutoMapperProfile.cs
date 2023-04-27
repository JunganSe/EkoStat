using EkoStatLibrary.Dtos;
using EkoStatApi.Models;

namespace EkoStatApi.Data;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ArticleRequestDto, Article>();
        CreateMap<Article, ArticleResponseDto>();

        CreateMap<EntryRequestDto, Entry>();
        CreateMap<Entry, EntryResponseDto>();

        CreateMap<TagRequestDto, Tag>();
        CreateMap<Tag, TagResponseDto>();

        CreateMap<UnitRequestDto, Unit>();
        CreateMap<Unit, UnitResponseDto>();

        CreateMap<UserRequestDto, User>();
        CreateMap<User, UserResponseDto>();
    }
}
