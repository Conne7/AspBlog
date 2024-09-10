using Blog.Application.DTO;
using Blog.DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class WritePostValidator : AbstractValidator<PostCreateDto>
    {
        public WritePostValidator(BlogContext ctx)
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("Max title length is 50!");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required!")
                .MaximumLength(500).WithMessage("Maximum number of characters are 500!");


            RuleFor(x => x.Tags)
                .Must(tags =>
                {
                    if (tags != null)
                    {
                        foreach (var tag in tags)
                        {
                            // Proveri da li trenutni tag postoji u bazi
                            var exists = ctx.Tags.Any(t => t.Name == tag);

                            // Ako neki tag ne postoji, validacija nije uspešna
                            if (!exists)
                            {
                                return false;
                            }
                        }
                    }
                    // Ako su svi tagovi validni, vraćamo true
                    return true;
                })
            .WithMessage("All tags must be valid!.");
        }
    }
}
