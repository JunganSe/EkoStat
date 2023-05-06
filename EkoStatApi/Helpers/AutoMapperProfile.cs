using EkoStatLibrary.Dtos;
using EkoStatApi.Models;

namespace EkoStatApi.Helpers;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ArticleRequestDto, Article>();
        CreateMap<Article, ArticleResponseDto>();

        CreateMap<EntryRequestDto, Entry>()
            .ForMember(dest => dest.Timestamp,
                opt => opt.MapFrom(src => TruncateToMinutes(src.Timestamp)));
        CreateMap<Entry, EntryResponseDto>();

        CreateMap<TagRequestDto, Tag>();
        CreateMap<Tag, TagResponseDto>();

        CreateMap<UnitRequestDto, Unit>();
        CreateMap<Unit, UnitResponseDto>();

        CreateMap<UserRequestDto, User>();
        CreateMap<User, UserResponseDto>();
    }

    private DateTime TruncateToMinutes(DateTime input) 
        => new DateTime(input.Year, input.Month, input.Day, input.Hour, input.Minute, 0);
}
