using Blog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        private readonly BlogContext _context;

        protected EfUseCase(BlogContext context)
        {
            _context = context;
        }

        protected BlogContext Context => _context;
    }
}
