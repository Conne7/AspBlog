using AutoMapper;
using Blog.Application;
using Blog.Application.DTO;
using Blog.Application.UseCases.Queries.Posts;
using Blog.DataAccess;
using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Blog.Implementation.UseCases.Queries.Posts
{
    public class EfGetPostsQuery : EfUseCase, IGetPostsQuery
    {
        private readonly IMapper _mapper;
        private readonly IApplicationActor _actor;


        public EfGetPostsQuery(BlogContext context,IMapper mapper, IApplicationActor actor) : base(context)
        {
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 10;

        public string Name => "Search posts";

        public string Description => "Search posts";

        public PagedResponse<PostDto> Execute(PostSearch search)
        {
            var query = Context.Posts.Include(x=>x.Author).Include(x=>x.Comments).Include(x=>x.PostTags).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Title.ToLower().Contains(search.Keyword.ToLower()) 
                || x.Author.Username.ToLower().Contains(search.Keyword.ToLower())
                || x.PostTags.Any(t=>t.Tag.Name.ToLower().Contains(search.Keyword.ToLower())));
            }

            if (search.MinComments.HasValue)
            {
                query = query.Where(x => x.Comments.Count > search.MinComments.Value);
            }

            if (search.UserId.HasValue)
            {
                query = query.Where(x => x.AuthorId == search.UserId.Value);
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(x => x.CreatedAt > search.DateFrom);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(x => x.CreatedAt < search.DateTo);
            }

            return query.AsPagedReponse<Post, PostDto>(search, _mapper);
        }
    }
}
