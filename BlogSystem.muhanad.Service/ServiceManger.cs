using AutoMapper;
using BlogSystem.muhanad.Abstractions;
using BlogSystem.muhanad.Abstractions.Auth;
using BlogSystem.muhanad.Abstractions.Comments;
using BlogSystem.muhanad.Abstractions.Mails;
using BlogSystem.muhanad.Abstractions.Posts;
using BlogSystem.muhanad.Domain.Contracts;
using BlogSystem.muhanad.Services.Auth;
using BlogSystem.muhanad.Services.Comments;
using BlogSystem.muhanad.Services.Posts;
using BlogSystem.muhanad.Shared.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Services
{
    public class ServiceManger( 
        UserManager<IdentityUser> userManager,
        IOptions<JwtOption> options,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMailService mailService,
        IConfiguration configuration
        ) : IServiceManger

    {
        public IAuthService AuthService { get; } = new AuthService(userManager, options,mailService,configuration);

        public IPostService PostService { get; } = new PostService(unitOfWork, userManager,mapper);

        public ICommentService CommentService { get; } = new CommentService(unitOfWork,mapper);
    }
}
