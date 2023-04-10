using EkoStatApi.Dtos;
using EkoStatApi.Models;

namespace EkoStatApi.Data;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ArticleRequestDto, Article>();
        CreateMap<Article, ArticleResponseDto>()
            .ForMember(
                dest => dest.EntryIds,
                opt => opt.MapFrom(src => src.Entries.Select(e => e.Id)));

        CreateMap<EntryRequestDto, Entry>();
        CreateMap<Entry, EntryResponseDto>();

        CreateMap<TagRequestDto, Tag>();
        CreateMap<Tag, TagResponseDto>()
            .ForMember(
                dest => dest.ArticleIds,
                opt => opt.MapFrom(src => src.Articles.Select(a => a.Id)));

        CreateMap<UnitRequestDto, Unit>();
        CreateMap<Unit, UnitResponseDto>();

        CreateMap<UserRequestDto, User>();
        CreateMap<User, UserResponseDto>()
            .ForMember(
                dest => dest.TagIds,
                opt => opt.MapFrom(src => src.Tags.Select(t => t.Id)))
            .ForMember(
                dest => dest.ArticleIds,
                opt => opt.MapFrom(src => src.Articles.Select(a => a.Id)))
            .ForMember(
                dest => dest.EntryIds,
                opt => opt.MapFrom(src => src.Entries.Select(e => e.Id)));
    }
}
