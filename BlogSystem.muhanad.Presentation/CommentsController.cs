using BlogSystem.muhanad.Abstractions;
using BlogSystem.muhanad.Shared.Dtos.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class commentsController(IServiceManger serviceManger) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(CreateCommentRequest request)
        {
           var userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           var result = await serviceManger.CommentService.CreateCommentAsync(request,userId);

            return Ok(result);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetComments(int postId)
        {

            var result = await serviceManger.CommentService.GetAllCommentsForPostAsync(postId);

            return Ok(result);
        }
    }
}
