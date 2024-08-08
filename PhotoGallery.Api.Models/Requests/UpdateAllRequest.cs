using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Api.Models.Requests
{
    public class UpdateAllRequest<T>
    {
        [Required]
        public T OldItem { get; set; }
        [Required]
        public T NewItem { get; set; }
    }
}
