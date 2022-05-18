using Framework.Interfaces;
using Framework.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PlayerService.Core.Abstractions.Repositories;
using PlayerService.Core.Abstractions.Services;
using PlayerService.Core.Domain.Mappers;
using PlayerService.Core.Persistance;
using PlayerService.Core.Persistance.IdentityService.Core.Persistance;
using PlayerService.Core.Persistance.Repositories;
using PlayerService.Core.Services;

namespace PlayerService
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
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IPlayerService, PlayerServices>();

            services.AddControllers();
            services.AddAutoMapper(config => config.AddProfile(typeof(PlayerMapper)));
            services.AddSwaggerGen(options => { options.SwaggerDoc("v1", new OpenApiInfo { Title = "News Service" }); });
            services.AddDbContext<PlayerDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
        }

        public void Configure(IApplicationBuilder app, PlayerDbContext dbContext)
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