using BlogSystem.muhanad.Shared.Dtos.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Abstractions.Comments
{
    public interface ICommentService
    {
        public Task<CommentResponse> CreateCommentAsync(CreateCommentRequest request, string authorId);
        public Task<IEnumerable<CommentResponse>>? GetAllCommentsForPostAsync(int postId);
    }
}
