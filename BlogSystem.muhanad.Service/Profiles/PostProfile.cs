using AutoMapper;
using BlogSystem.muhanad.Domain.Entites.Blogs;
using BlogSystem.muhanad.Shared.Dtos.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Services.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<BlogPost,PostResponse>()
                .ForMember(b => b.AuthorName,p => p.MapFrom( a => a.Author.UserName))
                .ForMember(b => b.CategoryName,p => p.MapFrom(a => a.Category.Name))
                .ForMember(b => b.Status,p => p.MapFrom(a => a.Status.ToString()))
                .ForMember(b => b.Tags,p => p.MapFrom(a => a.Tags.Select( n => n.Name).ToList()));
        }
    }
}
