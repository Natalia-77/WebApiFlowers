using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebFlower.Entities;
using WebFlower.Entities.Identity;

namespace WebFlower
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EFContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                //������������ ������:
                //������ �� ������ ���� � ���� �����.
                options.Password.RequireDigit = true;

                //�������� �������
                options.Password.RequiredLength = 6;

                //���� �� ������ ��������-������ �������.
                options.Password.RequireNonAlphanumeric = false;

                //������ �� ������ ���� � ���� ������ � ��������� ������
                options.Password.RequireUppercase = true;

                //������ �� ������ ���� � ���� ������ � �������� ������
                options.Password.RequireLowercase = true;


                //������������ ��� �����������:
                //�������� ������� � ���� �����������.
                options.User.AllowedUserNameCharacters ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                //���������� �� ������� ���� �������� ������ ��.�����.
                options.User.RequireUniqueEmail = false;

            })

             .AddEntityFrameworkStores<EFContext>()
                .AddDefaultTokenProviders();

            services.AddControllers();
            services.AddSwaggerGen();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            //����� �����,�� �������������� ����.
            string images = "Photos";
            var directory = Path.Combine(Directory.GetCurrentDirectory(), images);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(directory),
                    RequestPath = "/img"
                });
            app.ConfigMigrations();
            app.UseStaticFiles();               
                
                
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
