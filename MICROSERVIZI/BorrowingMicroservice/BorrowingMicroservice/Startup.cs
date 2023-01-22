using BorrowingMicroservice.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using BorrowingMicroservice.Services.Interfaces;
using BorrowingMicroservice.Services;
using BorrowingMicroservice.Data;
using BorrowingMicroservice.Helper;
using AutoMapper;

namespace BorrowingMicroservice
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BorriwingMicroservice", Version = "v1" });
            });
            services.AddDbContext<BorrowingContext>(op => op.UseSqlServer(Configuration.GetConnectionString("Database"),
                sqlServerOptionsAction: SqliteDbContextOptionsBuilderExtensions =>
                {
                    SqliteDbContextOptionsBuilderExtensions.EnableRetryOnFailure();
                }));

            services.AddAutoMapper(typeof(AutoMapProfile));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.ConfigureCors();

            services.AddScoped<IBorrowingServices, BorrowingServices>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BorrowingMicroservice v1"));

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
