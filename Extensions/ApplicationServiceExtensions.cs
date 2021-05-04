using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Data;
using ToDo.Interfaces;
using ToDo.Services;

namespace ToDo.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TaskContext>(x => x.UseSqlite(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}