using AutoMapper;
using BlogSystem.muhanad.Domain.Entites.Blogs;
using BlogSystem.muhanad.Shared.Dtos.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Services.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentResponse>()
                .ForMember(p => p.AuthorName , j => j.MapFrom( p => p.Author.UserName))
                .ForMember(p => p.PostTitle, j => j.MapFrom(p => p.Post.Title));
        }
    }
}
