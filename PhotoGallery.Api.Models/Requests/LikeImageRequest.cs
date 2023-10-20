using PhotoGallery.Api.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.Requests
{
    public class LikeImageRequest
    {
        [Required]
        public int AlbumId { get; set; }

        [Required]
        public int ImageId { get; set; }
        [Required]
        public string AlbumTitle { get; set; }
        [Required]
        public string AlbumDescription { get; set; }
        [Required]
        public string ImageExtension { get; set; }
        [Required]
        public string ImageTitle { get; set; }
        [Required]
        public string ImageDescription { get; set; }
    }
}
