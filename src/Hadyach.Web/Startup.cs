﻿using System.Reflection;
using AutoMapper;
using Hadyach.AutoMapper.Profiles;
using Swashbuckle.AspNetCore.Swagger;
using Hadyach.Data;
using Hadyach.Data.Entities;
using Hadyach.IoC.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hadyach.Web.Services;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;
using Hadyach.Web.Middleware;
using System.IO;
using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hadyach.Dtos.Articles;
using Hadyach.Validators.Articles;
using Hadyach.Dtos.Articles.Base;
using Hadyach.Dtos.Categories;
using Hadyach.Validators.Categories;
using Hadyach.Validators.Categories.Base;
using Hadyach.Validators.Articles.Base;
using Hadyach.Dtos.Categories.Base;
using Hadyach.Services.Resolvers;

namespace Hadyach.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.RegisterHadiachRepositories();
            services.RegisterHadiachServices();
            services.RegisterHadiachValidators();

            services.AddMvc()
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            
            services.AddAutoMapper(typeof(ArticleProfile).GetTypeInfo().Assembly, typeof(ParentCategoriesResolver).GetTypeInfo().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                //app.UseExceptionHandler();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
