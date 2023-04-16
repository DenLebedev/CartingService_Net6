using CartingService.DAL;
using CartingService.DAL.Interfaces;
using CartingService.DAL.LiteDb;

namespace CartingService.API.Extentions
{
    public static class LiteDbExtention
    {
        public static IServiceCollection AddLiteDbContext(this IServiceCollection services, string databasePasae)
        {
            services.AddTransient<IUnitOfWork, LiteDBUnitOfWork>();
            services.Configure<LiteDbOptions>(options => options.DatabaseLocation = databasePasae);

            return services;
        }
    }
}
