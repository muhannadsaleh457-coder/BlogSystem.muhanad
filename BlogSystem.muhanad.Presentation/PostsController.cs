using BlogSystem.muhanad.Abstractions;
using BlogSystem.muhanad.Shared.Dtos.Posts;
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
    public class postsController(IServiceManger serviceManger) : ControllerBase
    {

        [HttpPost]
        [Authorize(Roles ="Admin,Editor")]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await serviceManger.PostService.CreatePostAsync(request, userId);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> AllPosts([FromQuery] PostFilteration? request)
        {

            var result = await serviceManger.PostService.GetAllPostsAsync(request);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {

            var result = await serviceManger.PostService.GetPostByIdAsync(id);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> UpdatePost(UpdatePostRequest request,int id)
        {

            var result = await serviceManger.PostService.UpdatePostAsync(request,id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePost(int id)
        {

           var result = await serviceManger.PostService.DeletePostAsync(id);

            return Ok(result);
        }
    }
}
