using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.Requests
{
    public class UpdateAllRequest<T>
    {
        public T OldItem { get; set; }
        public T NewItem { get; set; }
    }
}
