using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public enum PostTypes
    {
        NotDefined = 1,
        CasualBlog = 2,
        Coffee = 3,
        Cooking = 4,
        Dating = 5,
        Invalid = 6
    }

   
    public class PostType
    {
        private int type;
        public int Type {
            get
            {
                return type;
            }
            set
            {
                type=SetPostType(value);
            }
        }

        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public int SetPostType(int type)
        {
            switch (type)
            {
                case 0:
                    type = 0;
                    break;
                case 1:
                    type = 1;
                    break;
                case 2:
                    type = 2;
                    break;
                case 3:
                    type = 3;
                    break;
                case 4:
                    type = 4;
                    break;
                default:
                    type = 99;
                    break;
            }
            return type;
        }
    }
}
