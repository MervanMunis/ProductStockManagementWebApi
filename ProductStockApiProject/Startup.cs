using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using ProductStockApiProject.Services;

namespace ProductStockApiProject
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
            // Configure services here
            services.AddControllers();

            // AutoMapper configuration
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ProductService>(); // To register ProductService
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
 
        }
    }
}
