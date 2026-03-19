using BlogSystem.muhanad.Domain.Contracts;
using BlogSystem.muhanad.Domain.Entites.Blogs;
using BlogSystem.muhanad.Presistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Presistence
{
    public class DbIntlizer(BlogDbContext _context,UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager) : IDbIntlizer
    {
        public async Task DbIntlizeAsync()
        {

            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
               await _context.Database.MigrateAsync();
            }

            if (!_context.Categories.Any())
             {

                var path = Path.Combine(
                      Directory.GetCurrentDirectory(),
                        "..",
                        "BlogSystem.muhanad.Presistence",
                        "Data",
                        "DataSedding",
                        "Category.json"
                         );

                var json = File.ReadAllText(path);
                var cateigories = JsonSerializer.Deserialize<List<Category>>(json);

                await _context.Categories.AddRangeAsync(cateigories);

                await _context.SaveChangesAsync();
                
            }

            if (!_context.Roles.Any())
            {

                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Editor"));
            }

            if (!_context.Users.Any()) 
            {

                var admin = new IdentityUser()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com"
                };

                var editor = new IdentityUser()
                {
                    UserName = "editor",
                    Email = "editor@gmail.com"
                };

                await userManager.CreateAsync(admin,"Abc@1234");
                await userManager.CreateAsync(editor, "Abc@1234");

                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(editor, "Editor");


            }


        }
    }
}
