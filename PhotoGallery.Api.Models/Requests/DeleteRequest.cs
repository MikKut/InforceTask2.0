using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.Requests
{
    public class DeleteRequest<T>
    {
        public T Item { get; set; }
    }
}
