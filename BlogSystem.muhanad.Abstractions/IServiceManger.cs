using BlogSystem.muhanad.Abstractions.Auth;
using BlogSystem.muhanad.Abstractions.Comments;
using BlogSystem.muhanad.Abstractions.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Abstractions
{
    public interface IServiceManger 
    {
        IAuthService AuthService { get; }
        IPostService PostService { get; }
        ICommentService CommentService { get; }

    }
}
