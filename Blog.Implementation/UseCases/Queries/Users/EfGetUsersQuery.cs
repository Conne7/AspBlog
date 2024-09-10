using AutoMapper;
using Blog.Application.DTO;
using Blog.DataAccess;
using Blog.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Application.UseCases.Queries.Users;

namespace Blog.Implementation.UseCases.Queries.Users
{
    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
        private readonly IMapper mapper;
        public EfGetUsersQuery(BlogContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public int Id => 3;

        public string Name => "Search Users";

        public string Description => "Search for users.";

        public PagedResponse<UserDto> Execute(UserSearch search)
        {
            var query = Context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Username.Contains(search.Keyword) ||
                                         x.Email.Contains(search.Keyword));
            }

            if (search.MinPosts.HasValue && search.MinPosts.Value >= 0)
            {
                query = query.Where(x => x.Posts.Count() > search.MinPosts.Value);
            }

            return query.AsPagedReponse<User, UserDto>(search, mapper);
        }
    }

}
