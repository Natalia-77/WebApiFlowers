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
        //public static string _url = "https://nat77.ga/img/";

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
                            Name="Afrodita Crocus",
                            Family="Rosasea elamia",
                            Weight=35,
                            //Image="https://nat77.ga/img/9.jpg" ,

                        },
                        //new Flower
                        //{
                        //    Name="Rosa Kira",
                        //    Family="Rosasea",
                        //    Weight=189,
                        //    Image= _url+"8.jpg", 
                        //},

                    };
                    context.Flowers.AddRange(flowers);
                    context.SaveChanges();
                }
            }
        }
    }
}
