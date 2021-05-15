using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebFlower.Entities.Domain;
using WebFlower.Entities.Identity;

namespace WebFlower.Entities
{
    public class EFContext:IdentityDbContext <AppUser, AppRole, long, IdentityUserClaim<long>,
                                              AppUserRole, IdentityUserLogin<long>,
                                              IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public EFContext ()           
        {
        }

        public EFContext(DbContextOptions<EFContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"Server=91.238.103.51;Port=5743;Database=nataliadb;User Id=natalia;Password=$544$B77w**G)K$t!Ube22}77b;");
            }

           
        }



        public DbSet<Flower> Flowers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
    

    
}
