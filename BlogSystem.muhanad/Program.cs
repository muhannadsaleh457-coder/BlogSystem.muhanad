
using BlogSystem.muhanad.Abstractions;
using BlogSystem.muhanad.Abstractions.Mails;
using BlogSystem.muhanad.Domain.Contracts;
using BlogSystem.muhanad.Presistence;
using BlogSystem.muhanad.Presistence.Contexts;
using BlogSystem.muhanad.Presistence.Repositories;
using BlogSystem.muhanad.Services;
using BlogSystem.muhanad.Services.Auth;
using BlogSystem.muhanad.Services.Email;
using BlogSystem.muhanad.Services.Profiles;
using BlogSystem.muhanad.Shared.Options;
using BlogSystem.muhanad.Web.Exstensions;
using BlogSystem.muhanad.Web.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogSystem.muhanad
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddServices(builder.Configuration);

            var app = builder.Build();

            var scope = app.Services.CreateScope();
           var dbIntlizer = scope.ServiceProvider.GetRequiredService<IDbIntlizer>();
            await dbIntlizer.DbIntlizeAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
