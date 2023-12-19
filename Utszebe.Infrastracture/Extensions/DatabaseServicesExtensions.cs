using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utszebe.Core.Interfaces;
using Utszebe.Infrastracture.Data;
using Utszebe.Infrastracture.Services;
using Utszebe.Infrastracture.Translation;

namespace Utszebe.Infrastracture.Extensions
{
    public static class DatabaseServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITranslator, Translator>();
            services.AddScoped<IDatabaseRepository, DatabaseRepository>();
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IQuerGeneratorAIModel, QuerGeneratorAIModel>();


            return services;
        }
    }
}
