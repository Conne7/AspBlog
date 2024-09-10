using AutoMapper;
using Blog.Application.DTO;
using Blog.Application.UseCases.Queries.Logs;
using Blog.DataAccess;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.LogSearch
{
    public class EfGetUseCaseLogs : EfUseCase, IGetUseCaseLogsQuery
    {
        private readonly IMapper mapper;

        public EfGetUseCaseLogs(BlogContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }
        public int Id => 5;

        public string Name => "Get UseCase logs";

        public string Description => "Get UseCase logs for every action users made, and filter it";

        public PagedResponse<UseCaseLogDto> Execute(UseCaseLogSearch search)
        {
            var query = Context.UseCaseLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.UseCaseName))
            {
                query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCaseName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.Username))
            {
                query = query.Where(x => x.Username.ToLower().Contains(search.Username.ToLower()));
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(x => x.ExecutedAt > search.DateFrom);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(x => x.ExecutedAt < search.DateTo);
            }

            return query.AsPagedReponse<UseCaseLog, UseCaseLogDto>(search, mapper);

        }
    }
}
