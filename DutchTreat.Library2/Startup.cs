using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Library2.DBContext;
using DutchTreat.Library2.Models.Data;
using DutchTreat.Library2.Models.Persistent;
using DutchTreat.Library2.Repositories;
using DutchTreat.Library2.Repositories.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DutchTreat.Library2
{
    public class Startup
    {
        private readonly IConfiguration localConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseContext"/> class.
        /// </summary>
        public Startup(IConfiguration config)
        {
            this.localConfig = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<DutchContext>();

            services.AddDbContext<DutchContext>(cfg=> {
                cfg.UseSqlServer(this.localConfig.GetConnectionString("DutchConnectionString"));
            });

            services.AddTransient<IEntityRepository<Product>, ProductsRepository>();
            services.AddTransient<IEntityRepository<Order>, OrdersRepository>();
            services.AddTransient<IEntityRepository<StoreUser>, StoreUserRepository>();
            services.AddTransient<DutchSeeder>();

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

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
