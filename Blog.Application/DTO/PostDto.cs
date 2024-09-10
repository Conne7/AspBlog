using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<string> Images { get; set; }
        public int Likes { get; set; }

        public IEnumerable<string> Tags { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class LikeDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string ProfileImage { get; set; }
    }

}
