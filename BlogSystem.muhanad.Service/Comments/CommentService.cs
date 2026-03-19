using AutoMapper;
using BlogSystem.muhanad.Abstractions.Comments;
using BlogSystem.muhanad.Domain.Contracts;
using BlogSystem.muhanad.Domain.Entites.Blogs;
using BlogSystem.muhanad.Domain.Exceptions.NotFound.Comments;
using BlogSystem.muhanad.Presistence.Repositories;
using BlogSystem.muhanad.Presistence.Spacefications;
using BlogSystem.muhanad.Shared.Dtos.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Services.Comments
{
    public class CommentService(IUnitOfWork unitOfWork , IMapper mapper) : ICommentService
    {
        public async Task<CommentResponse> CreateCommentAsync(CreateCommentRequest request,string authorId)
        {
            var comment = new Comment()
            {
                Content = request.Content,
                CreateAt = DateTime.Now,
                PostId = request.PostId,
                AuthorId = authorId
            };

            await unitOfWork.GetGenaricRepository<Comment, int>().AddAsync(comment);
            var count = await unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new Exception("error When save comment !!");

            return mapper.Map<CommentResponse>(comment);
        }

        public async Task<IEnumerable<CommentResponse>>? GetAllCommentsForPostAsync(int postId)
        {

            var includes = new List<Expression<Func<Comment, object>>>()
            {
                p => p.Author,
                p => p.Post
            };
            
            var spac = new Spacefication<Comment, int>()
            {
                Filteration = p => p.PostId == postId,
                Includes = includes
            };

            var comments = await unitOfWork.GetGenaricRepository<Comment, int>().GetAllAsync(spac);
            if (comments == null || !comments.Any()) throw new CommentsNotFound();

            return mapper.Map<IEnumerable<CommentResponse>>(comments);
        }
    }
}
