using Blog.Application.DTO;
using Blog.Application.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Queries.Comments
{
    public interface ICommentOnAPost : ICommand<CommentAPostDto>
    {
    }
}
