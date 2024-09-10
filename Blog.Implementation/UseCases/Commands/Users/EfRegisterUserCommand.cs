using Blog.Application.DTO;
using Blog.Application.UseCases.Users;
using Blog.Application.UseCases;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DataAccess;
using Blog.Implementation.Validators;
using FluentValidation;

namespace Blog.Implementation.UseCases.Commands.Users
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        public int Id => 2;

        public string Name => "UserRegistration";

        public string Description => "User registration and validation";

        private RegisterUserDtoValidator _validator;

        public EfRegisterUserCommand(BlogContext context, RegisterUserDtoValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public void Execute(RegisterUserDto data)
        {
            _validator.ValidateAndThrow(data);

            User user = new User
            {
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                //Photo = Context.Files.FirstOrDefault(x => x.Path.Contains("default")),
                Username = data.Username,
                UseCases = new List<UserUseCase>()
                {
                    new UserUseCase { UseCaseId = 3 },
                    new UserUseCase { UseCaseId = 5 }
                }
            };

            Context.Users.Add(user);

            Context.SaveChanges();
        }
    }

}
