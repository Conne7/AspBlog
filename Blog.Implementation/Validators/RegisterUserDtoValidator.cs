﻿using Blog.Application.DTO;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(BlogContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            if (ctx.Users != null)
            {

                RuleFor(x => x.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .Must(x => !ctx.Users.Any(u => u.Email == x))
                    .WithMessage("Email is already in use.");
                RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2);
                RuleFor(x => x.LastName).NotEmpty().MinimumLength(2);
                RuleFor(x => x.Password).NotEmpty().Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$")
                    .WithMessage("Minimum eight characters, at least one uppercase letter, one lowercase letter and one number:");
                RuleFor(x => x.Username)
                    .NotEmpty()
                    .Matches("(?=.{4,15}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
                    .WithMessage("Invalid username format.")
                    .Must(x => !ctx.Users.Any(u => u.Username == x))
                    .WithMessage("Username is already in use.");
            }

        }
    }

}
