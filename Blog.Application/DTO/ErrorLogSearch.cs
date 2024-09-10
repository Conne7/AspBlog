using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO
{
    public class ErrorLogSearch : PagedSearch
    {
        public string? ErrorId { get; set; }
        public string? Message { get; set; }
        public DateTime? DateFrom {get; set;}
        public DateTime? DateTo { get; set; }

    }
}
