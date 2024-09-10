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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.PostsCount, y => y.MapFrom(u => u.Posts.Count))
                .ForMember(x => x.LikesCount, y => y.MapFrom(u => u.CommentLikes.Count))
                .ForMember(x => x.CommentsCount, y => y.MapFrom(u => u.Comments.Count))
                .ForMember(x => x.ImagePath, y => y.MapFrom(u => u.Photo));
        }
    }

    public class UserLikeProfile : Profile
    {
        public UserLikeProfile()
        {
            CreateMap<PostLike, UserLikesDto>()
                .ForMember(x => x.PostTitle, y => y.MapFrom(u => u.Post.Title))
                .ForMember(x => x.VoteType, y => y.MapFrom(u => u.Vote == 1 ? "Like" : "Dislike"));
        }
    }
}
