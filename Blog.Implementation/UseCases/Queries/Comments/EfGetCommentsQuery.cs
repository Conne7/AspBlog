using AutoMapper;
using Blog.Application;
using Blog.Application.DTO;
using Blog.Application.UseCases.Queries.Comments;
using Blog.DataAccess;
using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Comments
{
    public class EfGetCommentsQuery : EfUseCase, IGetCommentsQuery
    {
        private readonly IMapper _mapper;
        private readonly IApplicationActor _actor;
        public EfGetCommentsQuery(BlogContext context, IMapper mapper, IApplicationActor actor) : base(context)
        {
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 11;

        public string Name => "Get comments";

        public string Description => "Get comments";

        public PagedResponse<CommentsDto> Execute(CommentsSearch search)
        {
            var query = Context.Comments.Include(x => x.Author).Include(x => x.Post).Include(x => x.Post).ThenInclude(x => x.PostTags).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Post.Title.ToLower().Contains(search.Keyword.ToLower())
                || x.Author.Username.ToLower().Contains(search.Keyword.ToLower())
                || x.Post.PostTags.Any(t => t.Tag.Name.ToLower().Contains(search.Keyword.ToLower()))
                || x.Text.ToLower().Contains(search.Keyword.ToLower()));
            }

            if (search.MinLikes.HasValue)
            {
                query = query.Where(x => x.Likes.Count() > search.MinLikes.Value);
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

            return query.AsPagedReponse<Comment, CommentsDto>(search, _mapper);
        }
    }
}
