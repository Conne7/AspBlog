using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class CommentLike : BaseEntity
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }

        public int Vote { get; set; } = 1;
    }
}
