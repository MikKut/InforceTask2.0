using PhotoGallery.Api.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.Requests
{
    public class LikeImageRequest
    {
        public string AlbumTitle { get; set; }
        public string AlbumDescription { get; set; }
        public string ImageExtension { get; set; }
        public string ImageTitle { get; set; }
        public string ImageDescription { get; set; }

    }
}
