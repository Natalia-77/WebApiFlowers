using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using WebFlower.Entities;
using WebFlower.Entities.Domain;
using WebFlower.Entities.Identity;
using WebFlower.RoleConstants;

namespace WebFlower
{
    public static class ConfigMigration
    {
        public static void ConfigMigrations(this IApplicationBuilder application)
        {
            using (var serviceScope = application.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var context = serviceScope.ServiceProvider.GetRequiredService<EFContext>();

                if (!roleManager.Roles.Any())
                {
                    var role = new AppRole
                    {
                        Name = Roles.Admin
                    };

                    var result = roleManager.CreateAsync(role).Result;
                }
               

                if (context.Flowers==null)
                    {
                    var flowers = new List<Flower>()
                    {
                        new Flower
                        {
                            Name="Rosa Prince",
                            Family="Rosasea",
                            Weight=158,
                            Image= "/img/6.jpg",

                        },
                        new Flower
                        {
                            Name="Rosa Kira",
                            Family="Rosasea",
                            Weight=189,
                            Image= "/img/9.jpg",
                        },

                    };
                    context.Flowers.AddRange(flowers);
                    context.SaveChanges();
                }
            }
        }
    }
}
