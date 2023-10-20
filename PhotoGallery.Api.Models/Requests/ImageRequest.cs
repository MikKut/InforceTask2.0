using PhotoGallery.Api.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.Requests
{
    public class ImageRequest
    {
        [Required]
        public AlbumDto Album { get; set; }
        [Required]
        public ImageDto Image { get; set; }
    }
}
