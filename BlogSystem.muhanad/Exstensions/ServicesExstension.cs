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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogSystem.muhanad.Web.Exstensions
{
    public static class ServicesExstension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();



            services.Configure<JwtOption>(configuration.GetSection("JwtOptions"));
            services.Configure<EmailSettingsOptions>(configuration.GetSection("EmailSettings"));



            services.AddIdentityCore<IdentityUser>(op =>
            {
                op.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<BlogDbContext>()
            .AddDefaultTokenProviders();




            services.AddScoped<IServiceManger, ServiceManger>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMailService, EmailService>();
            services.AddAutoMapper(a => a.AddProfile(new PostProfile()));
            services.AddAutoMapper(a => a.AddProfile(new CommentProfile()));


            services.AddDbContext<BlogDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IDbIntlizer, DbIntlizer>();


            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOption>();

            services.AddAuthentication(op =>
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

            return services;
        }
    }
}
