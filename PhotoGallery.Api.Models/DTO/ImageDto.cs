using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.DTO
{
    public class ImageDto
    {
        public string Extension { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
