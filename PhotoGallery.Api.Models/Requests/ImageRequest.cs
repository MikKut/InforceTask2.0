using PhotoGallery.Api.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.Requests
{
    public class ImageRequest
    {
        public AlbumDto Album { get; set; }
        public ImageDto Image { get; set; }
    }
}
