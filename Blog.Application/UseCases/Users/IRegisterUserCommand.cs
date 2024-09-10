using Blog.Application.DTO;
using Blog.Application.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Users
{
    public interface IRegisterUserCommand : ICommand<RegisterUserDto>
    {
    }
}
