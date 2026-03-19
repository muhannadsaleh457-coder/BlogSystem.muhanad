using BlogSystem.muhanad.Domain.Entites.Blogs;
using BlogSystem.muhanad.Shared.Dtos.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Abstractions.Posts
{
    public interface IPostService
    {
        public Task<PostResponse> CreatePostAsync(CreatePostRequest request, string authorId);
        public Task<IEnumerable<PostResponse>>? GetAllPostsAsync(PostFilteration? filteration); 
        public Task<PostResponse>? GetPostByIdAsync(int Id); 
        public Task<PostResponse>? UpdatePostAsync(UpdatePostRequest post,int postId); 
        public Task<bool> DeletePostAsync(int Id); 
    }
}
