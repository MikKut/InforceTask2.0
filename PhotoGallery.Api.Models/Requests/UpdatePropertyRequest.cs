using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.Requests
{
    public class UpdatePropertyRequest<ItemType, PropertyType>
    {
        [Required]
        public ItemType Item { get; set; }
        [Required]
        public PropertyType NewValue { get; set; }
    }
}
