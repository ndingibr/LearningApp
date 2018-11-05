using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using LearningApp.Core;
using LearningApp.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace LearningApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new AuthorizeFilter("Authenticated"));
            //});

            services.AddMvc();

            services.AddCors(
                options => options.AddPolicy("AllowCors",
                builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            })
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Contacts LearningApp.API", Version = "v1" });
            });

           

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidIssuer = Configuration["Tokens:Issuer"],
                       ValidAudience = Configuration["Tokens:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                   };
               });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Authenticated",
            //       policy => policy.RequireAuthenticatedUser());

            //    options.AddPolicy("Roles", policy =>
            //        policy.Requirements.Add(new UserInRoles("admin")));
            //});

            //services.AddSingleton<IAuthorizationHandler, UserInRolesHandler>();

            services.AddDbContext<LearningAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LearningAppConnection"), b => b.MigrationsAssembly("LearningApp.API")));
            services.AddDbContext<SecurityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SecurityConnection"), b => b.MigrationsAssembly("LearningApp.API")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ISecurityRepository<>), typeof(SecurityRepository<>));

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseCors("AllowCors");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Author}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My LearningApp.API V1");

            });
        }
    }
}
