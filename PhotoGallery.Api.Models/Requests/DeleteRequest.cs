using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.Requests
{
    public class DeleteRequest<T>
    {
        [Required]
        public T Item { get; set; }
    }
}
