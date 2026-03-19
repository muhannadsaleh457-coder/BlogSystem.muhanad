using BlogSystem.muhanad.Abstractions;
using BlogSystem.muhanad.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class authController(IServiceManger serviceManger) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest dto) 
        {

           var result = await serviceManger.AuthService.LoginAsync(dto);

            return Ok(result);
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterRequest dto)
        {

            var result = await serviceManger.AuthService.RegisterAsync(dto);

            return Ok(result);
        }

        [HttpPost("forgot-password")]

        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto dto)
        {

            await serviceManger.AuthService.ForgetPasswordSendEmail(dto);

            return Ok("Email Sent Successfuly");
        }

        [HttpPost("reset-password")]

        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {

            await serviceManger.AuthService.ResetPasswordAsync(dto);

            return Ok("Password Reset Succesfuly");
        }
    }
}
