using BlueToDo.Data;
using BlueToDo.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlueToDo.Models
{
    public class SeedData
    {
        public static void InitDatabase(IServiceProvider serviceProvider)
        {
            using
                (var context = new TodoContext(
                    serviceProvider.GetRequiredService<DbContextOptions<TodoContext>>()
                )
            )
            {
                context.Database.Migrate();

                var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = Enum.GetNames(enumType: typeof(RoleTypes));

                foreach (var role in roleNames)
                {
                    if (!RoleManager.RoleExistsAsync(role).Result)
                    {
                        RoleManager.CreateAsync(new IdentityRole { Name = role }).Wait();
                    }
                }
            }
        }
    }
}
