using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.Requests
{
    public class UpdatePropertyRequest<ItemType, PropertyType>
    {
        public ItemType Item { get; set; }
        public PropertyType NewValue { get; set; }
    }
}
