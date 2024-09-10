using AutoMapper;
using Blog.Application.DTO;
using Blog.Application.UseCases.Queries.Users;
using Blog.DataAccess;
using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Blog.Implementation.UseCases.Queries.Users
{
    public class EfGetUserLikesQuery :EfUseCase, IGetUserLikesQuery
    {
        private readonly IMapper mapper;
        public EfGetUserLikesQuery(BlogContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public int Id => 8;

        public string Name => "Get user likes";

        public string Description => "See what post user liked";

        public PagedResponse<UserLikesDto> Execute(UserSearch search)
        {
            var likes=Context.PostLikes.Include(x => x.Post).Where(x => x.UserId == search.UserId).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                likes = likes.Where(x => x.Post.Title.ToLower().Contains(search.Keyword.ToLower()) || x.Post.Content.ToLower().Contains(search.Keyword.ToLower()));
            }

            return likes.AsPagedReponse<PostLike, UserLikesDto>(search, mapper);

        }
    }
}
