using Blog.Api.DTO;
using Blog.Application;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;

        public LikeController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET: api/<LikeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET api/<LikeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LikeController>
        [Authorize]
        [HttpPost("post")]
        public IActionResult Post(
            [FromBody] DTO.PostLike like,
            [FromServices] BlogContext context,
            [FromServices] IApplicationActor actor)
        {

            int vote = 1;
            if (like.Type.ToLower() == "like")
            {
                vote = 1;
            }
            else if (like.Type.ToLower() == "dislike")
            {
                vote = -1;
            }
            else
            {
                return Conflict(new { error = "Only valid methods are like and dislike!" });
            }

            var post = context.Posts.Find(like.PostId);
            if (post != null)
            {
                var LikeExists = context.PostLikes.Where(x => x.PostId == like.PostId && x.UserId == actor.Id).FirstOrDefault();
                if (LikeExists != null)
                {
                    if (LikeExists.Vote == vote) context.PostLikes.Remove(LikeExists);
                    else LikeExists.Vote = vote; 
                }
                else
                {
                    Domain.PostLike likeObj = new Domain.PostLike();
                    likeObj.PostId = post.Id;
                    likeObj.Vote= vote;
                    likeObj.UserId=actor.Id;
                    context.PostLikes.Add(likeObj);
                   
                }
            }
            else return NotFound();

            context.SaveChanges();
            return Created();
        }

        // POST api/<LikeController>
        [Authorize]
        [HttpPost("comment")]
        public IActionResult LikeComm(
            [FromBody] DTO.CommentLike like,
            [FromServices] BlogContext context,
            [FromServices] IApplicationActor actor)
        {

            int vote = 1;
            if (like.Type.ToLower() == "like")
            {
                vote = 1;
            }
            else if (like.Type.ToLower() == "dislike")
            {
                vote = -1;
            }
            else
            {
                return Conflict(new { error = "Only valid methods are like and dislike!" });
            }

            var comment = context.Comments.Find(like.CommentId);
            if (comment != null)
            {
                var LikeExists = context.CommentLikes.Where(x => x.CommentId == like.CommentId && x.UserId == actor.Id).FirstOrDefault();
                if (LikeExists != null)
                {
                    if (LikeExists.Vote == vote) context.CommentLikes.Remove(LikeExists);
                    else LikeExists.Vote = vote;
                }
                else
                {
                    Domain.CommentLike likeObj = new Domain.CommentLike();
                    likeObj.CommentId = comment.Id;
                    likeObj.Vote = vote;
                    likeObj.UserId = actor.Id;
                    context.CommentLikes.Add(likeObj);

                }
            }
            else return NotFound();

            context.SaveChanges();
            return Created();
        }


        // PUT api/<LikeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LikeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
