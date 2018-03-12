using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Library2.DBContext;
using DutchTreat.Library2.Models;
using DutchTreat.Library2.Models.Data;
using DutchTreat.Library2.Models.Persistent;
using DutchTreat.Library2.Repositories;
using DutchTreat.Library2.Repositories.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DutchTreat.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseContext"/> class.
        /// </summary>
        public Startup(IConfiguration config)
        {
            this.Configuration = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddTransient<IEntityRepository<Product>, ProductsRepository>();
            services.AddTransient<IEntityRepository<Order>, OrdersRepository>();
            services.AddTransient<IEntityRepository<StoreUser>, StoreUserRepository>();
            services.AddDbContext<DutchContext>(cfg => {
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DutchConnectionString"));
            });


            services.AddTransient<DutchSeeder>();
            services.AddIdentity<StoreUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<DutchContext>();

            services.AddAuthentication()
                .AddCookie()
                 .AddJwtBearer(cfg =>
                 {
                     cfg.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidIssuer = this.Configuration["Tokens:Issuer"],
                         ValidAudience = this.Configuration["Tokens:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Tokens:Key"]))
                     };
                 });

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //seed the dataBase
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                    seeder.Seed().Wait();
                }
            }

            app.UseAuthentication();

            app.UseMvc(cfg => {
                cfg.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "Products", Action = "GetAll" });
            });
        }
    }
}
