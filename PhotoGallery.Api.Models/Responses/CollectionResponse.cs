using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.Responses
{
    public class CollectionResponse<T>
        where T : class
    {
        public IEnumerable<T> Items { get; set; }
    }
}
