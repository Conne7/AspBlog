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
    public class EfGetErrorLogs : EfUseCase, IGetErrorLogsQuery
    {
        private readonly IMapper mapper;
        public EfGetErrorLogs(BlogContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public int Id => 6;

        public string Name => "Get Errors logs";

        public string Description => "Get Errors logs for every error users made, and filter it";

        public PagedResponse<ErrorLogDto> Execute(ErrorLogSearch search)
        {
            var query = Context.ErrorLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.Message))
            {
                query = query.Where(x => x.Message.ToLower().Contains(search.Message.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.ErrorId))
            {
                query = query.Where(x => x.ErrorId.ToString().ToLower().Contains(search.ErrorId.ToLower()));
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(x => x.Time > search.DateFrom);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(x => x.Time < search.DateTo);
            }

            return query.AsPagedReponse<ErrorLog, ErrorLogDto>(search, mapper);

        }
    }
}
