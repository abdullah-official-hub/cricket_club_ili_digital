using Framework.Interfaces;
using Framework.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NewsService.Core.Abstractions.Repositories;
using NewsService.Core.Abstractions.Services;
using NewsService.Core.Domain.Mappers;
using NewsService.Core.Persistance;
using NewsService.Core.Persistance.IdentityService.Core.Persistance;
using NewsService.Core.Persistance.Repositories;
using NewsService.Core.Services;

namespace NewsService
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<INewsService, NewsServices>();

            services.AddControllers();
            services.AddAutoMapper(config => config.AddProfile(typeof(NewsMapper)));
            services.AddSwaggerGen(options => { options.SwaggerDoc("v1", new OpenApiInfo { Title = "News Service" }); });
            services.AddDbContext<NewsDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
        }

        public void Configure(IApplicationBuilder app, NewsDbContext dbContext)
        {
            dbContext.Database.Migrate();
            app.UseMiddleware<ExceptionHandler>();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}