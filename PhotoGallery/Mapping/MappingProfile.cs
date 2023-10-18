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
            CreateMap<User, LoginRequest>();
            CreateMap<LoginRequest, User>();
            // Image mapping
            CreateMap<ImageDto, Image>()
                .ForMember(dest => dest.ImageId, opt => opt.Ignore()) // Ignore ImageId when mapping from ImageDto to Image
                .ForMember(dest => dest.Album, opt => opt.Ignore()) // Ignore Album when mapping from ImageDto to Image
                .ReverseMap(); // Create a reverse mapping configuration

            CreateMap<Image, ImageDto>(); // Map Image to ImageDto

            // Album mapping
            CreateMap<AlbumDto, Album>()
                .ForMember(dest => dest.AlbumId, opt => opt.Ignore()) // Ignore AlbumId when mapping from AlbumDto to Album
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ReverseMap(); // Create a reverse mapping configuration

            CreateMap<AlbumWithImageDto, Album>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.AlbumDto.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.AlbumDto.Description))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => new List<Image>
            {
                new Image()
                {
                    Extension = src.Image.Extension,
                    Title = src.Image.Title,
                    Description = src.Image.Description,
                    Dislikes = src.Image.Dislikes,
                    Likes = src.Image.Likes,
                    Content = src.Image.Content,

                }
            }))
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ReverseMap();

        }
    }
}
