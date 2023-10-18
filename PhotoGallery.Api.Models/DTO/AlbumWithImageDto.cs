using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.DTO
{
    public class AlbumWithImageDto
    {
        public AlbumDto AlbumDto { get; set; }
        public ImageDto Image { get; set; }
    }
}
