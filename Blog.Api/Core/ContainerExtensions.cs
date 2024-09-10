using Blog.Application;
using Blog.Application.UseCases.Commands.Posts;
using Blog.Application.UseCases.Commands.users;
using Blog.Application.UseCases.Queries.Comments;
using Blog.Application.UseCases.Queries.Logs;
using Blog.Application.UseCases.Queries.Posts;
using Blog.Application.UseCases.Queries.Users;
using Blog.Application.UseCases.Users;
using Blog.Implementation;
using Blog.Implementation.UseCases.Commands.Posts;
using Blog.Implementation.UseCases.Commands.Users;
using Blog.Implementation.UseCases.Logging;
using Blog.Implementation.UseCases.Queries.LogSearch;
using Blog.Implementation.UseCases.Queries.Posts;
using Blog.Implementation.UseCases.Queries.Users;
using Blog.Implementation.Validators;
using System.IdentityModel.Tokens.Jwt;

namespace Blog.Api.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            //Validators
            services.AddTransient<RegisterUserDtoValidator>();
            services.AddTransient<UpdateUserAccessDtoValidator>();
            services.AddTransient<WritePostValidator>();
            services.AddTransient<WiteCommentValidator>();

            //Commands and queries
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<IUpdateUseAccessCommand, EfUpdateUserAccessCommand>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IUseCaseLogger, SPUseCaseLogger>();
            services.AddTransient<IGetUseCaseLogsQuery, EfGetUseCaseLogs>();
            services.AddTransient<IGetErrorLogsQuery, EfGetErrorLogs>();
            services.AddTransient<IWritePostCommand, EfWritePostCommand>();
            services.AddTransient<IGetPostQuery, EfGetPostQuery>();
            services.AddTransient<IGetUserLikesQuery, EfGetUserLikesQuery>();
            services.AddTransient<ICommentOnAPost, EfCommentOnAPost>();
            services.AddTransient<IGetPostsQuery, EfGetPostsQuery>();
            //EfGetUseCaseLogs

        }

        public static Guid? GetTokenId(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader.Split("Bearer ").Length != 2)
            {
                return null;
            }

            string token = authHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var claim = claims.First(x => x.Type == "jti").Value;

            var tokenGuid = Guid.Parse(claim);

            return tokenGuid;
        }
    }

}
