using Framework.Interfaces;
using Framework.Middlewares;
using IdentityService.Core.Abstractions.Repositories;
using IdentityService.Core.Abstractions.Services;
using IdentityService.Core.Domain.Mappers;
using IdentityService.Core.Persistance;
using IdentityService.Core.Persistance.IdentityService.Core.Persistance;
using IdentityService.Core.Persistance.Repositories;
using IdentityService.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace IdentityService
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
            services.AddTransient<IIdentityRepository, IdentityRepository>();
            services.AddTransient<IIdentityService, IdentityServices>();

            services.AddControllers();
            services.AddAutoMapper(config => config.AddProfile(typeof(IdentityMapper)));
            services.AddSwaggerGen(options => { options.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity Service" }); });
            services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
        }

        public void Configure(IApplicationBuilder app, IdentityDbContext dbContext)
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