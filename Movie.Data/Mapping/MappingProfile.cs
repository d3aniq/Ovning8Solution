using AutoMapper;
using Movie.Core.DTOs;
using Movie.Core.Entities;

namespace Movie.Data.Mapping
{
    /// <summary>
    /// AutoMapper profile configuration for mapping between domain entities and DTOs.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {



            CreateMap<VideoMovie, MovieDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Name : null));
            CreateMap<VideoMovie, MovieDetailDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Name : null));
            CreateMap<MovieDetails, MovieDetailsDto>();
            CreateMap<Actor, ActorDto>();
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Comment));

            // Reverse mappings for updates/creates
            //CreateMap<MovieDto, VideoMovie>();
            //CreateMap<MovieDto, VideoMovie>()
              //  .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => new Genre { Name = src.Genre }));
            CreateMap<MovieDto, VideoMovie>()
                .ForMember(dest => dest.Genre, opt => opt.Ignore());

            CreateMap<MovieDetailsDto, MovieDetails>();
        }
    }
}