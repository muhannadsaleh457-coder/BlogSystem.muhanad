using BlogSystem.muhanad.Abstractions.Auth;
using BlogSystem.muhanad.Abstractions.Mails;
using BlogSystem.muhanad.Domain.Exceptions.BadRequest;
using BlogSystem.muhanad.Domain.Exceptions.BadRequest.Auth;
using BlogSystem.muhanad.Domain.Exceptions.NotFound.Auth;
using BlogSystem.muhanad.Domain.Exceptions.UnuAuthrize;
using BlogSystem.muhanad.Shared.Dtos.Auth;
using BlogSystem.muhanad.Shared.Dtos.Emails;
using BlogSystem.muhanad.Shared.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Services.Auth
{
    public class AuthService(
        UserManager<IdentityUser> userManager,
        IOptions<JwtOption> options,
        IMailService mailService,
        IConfiguration configuration
        ) : IAuthService
    {
     

        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserUnAuthrize();
            var flag = await userManager.CheckPasswordAsync(user, request.Password);

            if (!flag) throw new UserUnAuthrize();

            return new UserResponse
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is not null) throw new RegisterBadRequest();

            user = new IdentityUser()
            {
                Email = request.Email,
                UserName = request.UserName,
            };
            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Server Error Try Again !!");
            }

            return new UserResponse
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = await GenerateTokenAsync(user)
            };

        }

        async Task<string> GenerateTokenAsync(IdentityUser user)
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(options.Value.SigningKey));

            var token = new JwtSecurityToken
                (
                  issuer: options.Value.Issuer,
                  audience: options.Value.Audience,
                  claims : claims,
                  expires: DateTime.Now.AddDays(options.Value.Lifetime),
                  signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
                );

            var tokenHandler = new JwtSecurityTokenHandler();


            return tokenHandler.WriteToken(token);
        }
        public async Task ForgetPasswordSendEmail(ForgetPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.email);
            if (user is null) throw new UserUnAuthrize();

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(
             Encoding.UTF8.GetBytes(token)
            );

            var link = $"{configuration["BaseUrl"]}api/reset-password?email={dto.email}&token={encodedToken}";

            var email = new EmailDto()
            {
                To = dto.email,
                Subject = "Reset Password",
                Body = $"Click here to reset your password:\n {link}"
            };
            await mailService.SendEmail(email);
        }
        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.email);
            if (user is null) throw new UserUnAuthrize();

            var result = await userManager.ResetPasswordAsync(user, dto.token, dto.newPassword);

            if (!result.Succeeded)
                throw new BadRequestException("Try Again !!");

        }

    } 
}
