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
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(u => u.PostTypeId))
                .ForMember(x => x.Images, y => y.MapFrom(u => u.PostImages.Select(x=>x.Image).ToList()))
                .ForMember(x => x.Likes, y => y.MapFrom(u => u.Likes.Count))
                .ForMember(x => x.Author, y => y.MapFrom(u => u.Author.Username))
                .ForMember(x => x.Tags, y => y.MapFrom(u => u.PostTags.Select(x=>x.Tag.Name).ToList()))
                .ForMember(x=>x.CategoryName, y=>y.MapFrom(u=>u.PostType.Name));

            //                .ForMember(x => x.Content, y => y.MapFrom(u => u.Content))

        }


    }

    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentsDto>()
                .ForMember(x => x.Text, y => y.MapFrom(u => u.Text))
                .ForMember(x => x.ParentId, y => y.MapFrom(u => u.Parent))
                .ForMember(x => x.ParentId, y => y.MapFrom(u => u.ParentId));

            //                .ForMember(x => x.Content, y => y.MapFrom(u => u.Content))

        }


    }
}
