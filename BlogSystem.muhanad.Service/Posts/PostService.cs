
using AutoMapper;
using Azure.Core;
using BlogSystem.muhanad.Abstractions.Posts;
using BlogSystem.muhanad.Domain.Contracts;
using BlogSystem.muhanad.Domain.Entites.Blogs;
using BlogSystem.muhanad.Domain.Exceptions.BadRequest;
using BlogSystem.muhanad.Domain.Exceptions.NotFound.Posts;
using BlogSystem.muhanad.Presistence.Spacefications;
using BlogSystem.muhanad.Shared.Dtos.Posts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Services.Posts
{
    public class PostService(IUnitOfWork unitOfWork,UserManager<IdentityUser> userManager,IMapper mapper) : IPostService

    {
        public async Task<PostResponse> CreatePostAsync(CreatePostRequest request,string authorId)
        {
            var status = (BlogPostStatus)Enum.Parse(typeof(BlogPostStatus), request.Status);

            var post = new BlogPost(request.Title,request.Content,status,request.CategoryId,authorId);

           await unitOfWork.GetGenaricRepository<BlogPost,int>().AddAsync(post);

            var count = await unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new Exception();

            var user = await userManager.FindByIdAsync(authorId);
           
            var postResponse = new PostResponse()
            {
                AuthorName = user.UserName,
                Title = request.Title,
                Content = request.Content,
                CrerateAt = DateTime.Now,
                Status = status.ToString()

            };

            if (request.CategoryId is not null) 
            {
                var category = await unitOfWork.GetGenaricRepository<Category, int>().GetByIdAsync(request.CategoryId.Value);
                if (category is null)  throw new CategoryNotFound(request.CategoryId.Value);

                postResponse.CategoryName = category.Name;
            }

            return postResponse;
        }

        public async Task<IEnumerable<PostResponse>>? GetAllPostsAsync(PostFilteration? filteration)
        {

            var space = new Spacefication<BlogPost, int>();

            if (filteration is null)
            {
 

              var result =  await unitOfWork.GetGenaricRepository<BlogPost, int>().GetAllAsync(space);

                if (result is null) throw new PostsNotFoundException();

                return mapper.Map<List<PostResponse>>(result);
            }

            var includes = new List<Expression<Func<BlogPost, object>>>()
            {
                p => p.Author,
                p => p.Category,
                p => p.Tags
            };

            space.Filteration = p => (!filteration.CategoryId.HasValue || p.CategoryId == filteration.CategoryId)
                  && (string.IsNullOrEmpty(filteration.Status) || p.Status == Enum.Parse<BlogPostStatus>(filteration.Status));

            space.Includes = includes;
           
            var posts =  await unitOfWork.GetGenaricRepository<BlogPost, int>().GetAllAsync(space);
            if (posts is null|| !posts.Any()) throw new PostsNotFoundException();

           return mapper.Map<List<PostResponse>>(posts);
        }

        public async Task<PostResponse>? GetPostByIdAsync(int Id)
        {
            var includes = new List<Expression<Func<BlogPost, object>>>()
            {
                p => p.Author,
                p => p.Category,
                p => p.Tags
            };

            var space = new Spacefication<BlogPost, int>()
            {
                Filteration = p => p.Id == Id,
                Includes = includes
            };

            var post = await unitOfWork.GetGenaricRepository<BlogPost, int>().GetByIdAsync(space);
            if (post is null) throw new PostNotFound(Id);

            return mapper.Map<PostResponse>(post);
        }

        public async Task<PostResponse>? UpdatePostAsync(UpdatePostRequest request,int postId)
        {
            var includes = new List<Expression<Func<BlogPost, object>>>()
            {
                p => p.Author,
                p => p.Category,
                p => p.Tags
            };

            var space = new Spacefication<BlogPost, int>()
            {
                Filteration = p => p.Id == postId,
                Includes = includes
            };

            var post = await unitOfWork.GetGenaricRepository<BlogPost,int>().GetByIdAsync(space);

            post.Title = request.Title;
            post.Content = request.Content;
            post.CategoryId = request.CategoryId;
            post.Status = Enum.Parse<BlogPostStatus>(request.Status);
            post.UpdatedAt = DateTime.Now;

             unitOfWork.GetGenaricRepository<BlogPost,int>().Update(post);
            var count = await unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new Exception();

            return mapper.Map<PostResponse>(post);
        }

        public async Task<bool> DeletePostAsync(int Id)
        {

            var post = await unitOfWork.GetGenaricRepository<BlogPost, int>().GetByIdAsync(Id);

            unitOfWork.GetGenaricRepository<BlogPost, int>().Delete(post);
            var count = await unitOfWork.SaveChangesAsync();

            if (count <= 0) return false;

            return true;

        }

    }
}
