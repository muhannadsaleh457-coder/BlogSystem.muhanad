
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

            builder.Services.AddControllers();
           
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

           

            builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("JwtOptions"));
            builder.Services.Configure<EmailSettingsOptions>(builder.Configuration.GetSection("EmailSettings"));



            builder.Services.AddIdentityCore<IdentityUser>(op =>
            {
                op.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<BlogDbContext>()
            .AddDefaultTokenProviders(); 
             
            


            builder.Services.AddScoped<IServiceManger, ServiceManger>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IMailService,EmailService>();
            builder.Services.AddAutoMapper(a => a.AddProfile(new PostProfile()));
            builder.Services.AddAutoMapper(a => a.AddProfile(new CommentProfile()));


            builder.Services.AddDbContext<BlogDbContext>( op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDbIntlizer, DbIntlizer>();


            var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOption>();

            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = "Bearer";
                op.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                };
            });

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
