using Blog.Application.DTO;
using Blog.Application.UseCases.Commands.users;
using Blog.Application.UseCases.Queries.Users;
using Blog.Application.UseCases.Users;
using Blog.DataAccess;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;

        public UsersController(UseCaseHandler commandHandler)
        {
            _useCaseHandler = commandHandler;
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDto dto, [FromServices] IRegisterUserCommand cmd)
        {
            _useCaseHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] UserSearch search, [FromServices] IGetUsersQuery query)
            => Ok(_useCaseHandler.HandleQuery(query, search));

        [Authorize]
        [HttpGet("{id}/likes")]
        public IActionResult GetLikes(int id,[FromQuery] UserSearch search, [FromServices] IGetUserLikesQuery query)
        {
            search.UserId = id;

            return Ok(_useCaseHandler.HandleQuery(query, search));

        }

        [HttpGet("{id}/comments")]
        [Authorize]
        public IActionResult GetComments ([FromQuery] UserSearch search, [FromServices] IGetUsersQuery query)
=> Ok(_useCaseHandler.HandleQuery(query, search));

        //PUT /api/users/5/access
        [HttpPut("{id}/access")]
        public IActionResult ModifyAccess(int id, [FromBody] UpdateUserAccessDto dto,
                                                  [FromServices] IUpdateUseAccessCommand command)
        {
            dto.UserId = id;
            _useCaseHandler.HandleCommand(command, dto);

            return NoContent();
        }

    }
}
