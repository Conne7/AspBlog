using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class PostImage : BaseEntity
    {

        public string Image { get; set; }
        public string Alt { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
