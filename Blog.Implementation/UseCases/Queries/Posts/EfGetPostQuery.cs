using AutoMapper;
using Blog.Application;
using Blog.Application.DTO;
using Blog.Application.Exceptions;
using Blog.Application.UseCases.Queries.Posts;
using Blog.DataAccess;
using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Posts
{
    public class EfGetPostQuery : EfUseCase, IGetPostQuery
    {
        private readonly IMapper _mapper;
        private readonly IApplicationActor _actor;
        public EfGetPostQuery(BlogContext context, IMapper mapper, IApplicationActor actor) : base(context)
        {
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 8;

        public string Name => "View Post";

        public string Description => "View a specific post";

        public PostDto Execute(int search)
        {
            IQueryable<Post> posts = Context.Posts.Include(x=>x.PostTags).Include(x=>x.PostImages).Include(x=>x.Author).Include(x=>x.PostType).Include(x=>x.Comments).Include(x=>x.Likes).Where(x=>x.IsActive).Where(x=>x.Id==search);

            var dto = posts.FirstOrDefault();

            if (dto == null)
            {
                throw new EntityNotFoundException(nameof(posts), search);
            }

            return new PostDto
            {
                Id = search,
                Title = dto.Title,
                Content = dto.Content,
                CategoryId = dto.PostTypeId,
                CategoryName = dto.PostType.Name,
                CreatedAt = dto.CreatedAt,
                Images = dto.PostImages.Select(x => x.Image).ToList(),
                Tags = dto.PostTags.Select(x => x.Tag.Name).ToList(),
                Likes = dto.Likes.Count(),
                Author = dto.Author.Username,
            };
            
        }
    }
}
