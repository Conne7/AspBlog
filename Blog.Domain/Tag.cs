using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class Tag : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }

    }
}
