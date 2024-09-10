using Blog.Application;
using Blog.Application.DTO;
using Blog.Application.UseCases.Commands.Posts;
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
    public class EfWritePostCommand : EfUseCase, IWritePostCommand
    {
        private WritePostValidator _validator;

        private readonly IApplicationActor _actor;
        public EfWritePostCommand(BlogContext context,
             WritePostValidator validator, IApplicationActor actor) : base(context)
        {
            _validator = validator;
            _actor = actor;
        }
        public int Id => 7;

        public string Name => "Write post";

        public string Description => "Write post";

      

        public void Execute(PostCreateDto data)
        {
            _validator.ValidateAndThrow(data);


            Post newPost = new Domain.Post
            {
                AuthorId = _actor.Id,
                Content = data.Content,
                Title = data.Title,
                CreatedAt = DateTime.Now,
                IsActive = true,
                PostTypeId = data.Type == null ? 1 : Context.PostTypes.Where(x=>x.Name==data.Type).FirstOrDefault() == null ? 6 : Context.PostTypes.Where(x => x.Name == data.Type).FirstOrDefault().Type
            };

            Context.Posts.Add(newPost);
            Context.SaveChanges();

            if (data.Tags != null)
            {

                int id = newPost.Id;

                foreach (var t in data.Tags)
                {
                    PostTag tag = new PostTag
                    {
                        PostId = id,
                        TagId = Context.Tags.Where(x => x.Name == t).First().Id
                    };
                    Context.PostTags.Add(tag);
                }

                Context.SaveChanges();
            }


        }
    }
}
