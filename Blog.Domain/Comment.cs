using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class Comment : Entity
    {
        public int AuthorId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public virtual User Author { get; set; }
        public virtual Post Post { get; set; }

        public virtual Comment? Parent { get; set; }
        public int? ParentId { get; set; }
        public virtual IEnumerable<CommentLike> Likes { get; set; } = new HashSet<CommentLike>();
        public virtual IEnumerable<Comment> Replies { get; set; } = new HashSet<Comment>();
    }
}
