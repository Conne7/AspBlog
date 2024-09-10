using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO
{
    public class UserSearch : PagedSearch
    {
        public string? Keyword { get; set; }
        public int? MinPosts { get; set; }

        public int? UserId { get; set; }
    }
}
