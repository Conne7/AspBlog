using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO
{
    public class CommentAPostDto
    {
        public int PostId { get; set; }
        public string Comment { get; set; }
        public int? CommentId {get; set;}
    }
}
