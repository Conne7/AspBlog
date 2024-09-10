using Blog.Application.DTO;
using Blog.Application.UseCases.Queries;
using Blog.Application.UseCases.Queries.Logs;
using Blog.DataAccess;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;

        public LogController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET: api/<LikeController>
        [HttpGet("errors")]
        public IActionResult GetErrors([FromQuery] ErrorLogSearch search, [FromServices] IGetErrorLogsQuery query)
        => Ok(_useCaseHandler.HandleQuery(query, search));

        [HttpGet("usecases")]
        public IActionResult GetUseCases([FromQuery] UseCaseLogSearch search, [FromServices] IGetUseCaseLogsQuery query)
                    => Ok(_useCaseHandler.HandleQuery(query, search));


        //Napravio sam i ovaj kontroler radi prakticnosti, da ne mora neko da salje parametar, vec da postoji i ova varijanta, a preko parametra se radi LIKE, ovde mora da unese tacan ErrorId
        // GET api/<LogController>/5
        [HttpGet("errors/{id}")]
        public IActionResult Get(string id,
            [FromServices] BlogContext context)
        {
            var log = context.ErrorLogs.Where(x => x.ErrorId.Equals(Guid.Parse(id)));

            if (log != null)
                return Ok(new {
                     message = log.First().Message,
                     time = log.First().Time
                    });
            else return NotFound();
            
        }

        // POST api/<LogController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
