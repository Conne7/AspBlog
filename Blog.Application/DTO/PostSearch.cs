using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO
{
    public class PostSearch : PagedSearch
    {
        public string? Keyword { get; set; }
        public int? MinComments { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

    }

    public class CommentsSearch : PagedSearch
    {
        public string? Keyword { get; set; }
        public int? MinLikes { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

    }
}
