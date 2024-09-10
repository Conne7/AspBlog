using Blog.Application.DTO;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class WiteCommentValidator : AbstractValidator<CommentAPostDto>
    {
        public WiteCommentValidator(BlogContext ctx)
        {
            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Content is required!")
                .MaximumLength(100).WithMessage("Maximum number of characters are 500!");
        }
    }
}
