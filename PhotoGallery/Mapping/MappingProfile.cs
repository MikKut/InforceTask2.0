using AutoMapper;
using PhotoGallery.Api.Models.DTO;
using PhotoGallery.Api.Models.Entities;
using PhotoGallery.Api.Models.Requests;

namespace PhotoGallery.Api.Host.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginRequest>().ReverseMap();

            CreateMap<ImageDto, Image>().ReverseMap();

            CreateMap<AlbumDto, Album>().ReverseMap();

            CreateMap<AlbumWithImageDto, Album>()
                .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.AlbumDto.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.AlbumDto.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.AlbumDto.Description))
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Album, AlbumWithImageDto>()
                .ForMember(dest => dest.AlbumDto, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault())); // Assuming there's only one image

        }
    }

}
