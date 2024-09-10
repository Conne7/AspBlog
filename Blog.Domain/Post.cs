using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class Post : Entity
    {
        public int AuthorId { get; set; }
        public int PostTypeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual User Author { get; set; }
        public virtual PostType PostType { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<PostLike> Likes { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; }

    }
}
