using AutoMapper;
using Blog.Application.DTO;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Profiles
{
    public class UseCaseProfile : Profile
    {
        public UseCaseProfile()
        {
            CreateMap<UseCaseLog, UseCaseLogDto>()
                .ForMember(x => x.Username, y => y.MapFrom(u => u.Username))
                .ForMember(x => x.UseCaseName, y => y.MapFrom(u => u.UseCaseName))
                .ForMember(x => x.ExecutedAt, y => y.MapFrom(u => u.ExecutedAt))
                .ForMember(x => x.LogId, y => y.MapFrom(u => u.LogId));
        }
    }

    public class ErrorsProfile : Profile
    {
        public ErrorsProfile()
        {
            CreateMap<ErrorLog, ErrorLogDto>()
                .ForMember(x => x.Message, y => y.MapFrom(u => u.Message))
                .ForMember(x => x.Time, y => y.MapFrom(u => u.Time));
        }
    }
}
