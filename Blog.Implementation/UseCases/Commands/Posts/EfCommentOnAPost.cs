using Blog.Application;
using Blog.Application.DTO;
using Blog.Application.Exceptions;
using Blog.Application.UseCases.Queries.Comments;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Posts
{
    public class EfCommentOnAPost : ICommentOnAPost
    {
        BlogContext _context;
        WiteCommentValidator _validator;
        IApplicationActor _actor;
        public EfCommentOnAPost(BlogContext context, WiteCommentValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }
        public int Id => 9;

        public string Name => "Comment on a post";

        public string Description => "Leave your comment on a post, or reply to another one. User can comment only once per minute, on a same post";

        public void Execute(CommentAPostDto data)
        {

            var post = _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefault(p => p.Id == data.PostId);

           

            if (post == null) throw new EntityNotFoundException(nameof(data.PostId),data.PostId);

            if (post?.Comments != null && post.Comments.Any(c => c.AuthorId == _actor.Id && c.CreatedAt > DateTime.UtcNow.AddMinutes(-1)))
            {
                throw new ConflictException("You can only comment once per minute on the same post!");
            }

            if(post?.Comments!=null && post.Comments.Any(c=>c.AuthorId==_actor.Id && c.Text.ToLower() == data.Comment.ToLower()))
            {
                throw new ConflictException("You can't write the same comment multiple times!!");
            }

            _validator.ValidateAndThrow(data);

            if(data.CommentId.HasValue && post.Comments.Where(x=>x.Id==data.CommentId)==null) throw new EntityNotFoundException(nameof(data.CommentId), data.CommentId.Value);

            Comment newComment = new Comment();
            newComment.Text = data.Comment;
            newComment.CreatedAt = DateTime.UtcNow;
            newComment.AuthorId = _actor.Id;
            newComment.PostId = data.PostId;

            if (data.CommentId.HasValue) newComment.ParentId = data.CommentId;
            
            _context.Comments.Add(newComment);
            _context.SaveChanges();

        }
    }
}
