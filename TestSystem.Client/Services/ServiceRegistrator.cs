using TestSystem.Client.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestSystem.Core.Models;

namespace TestSystem.Client.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IDataService<ImageWithText>, DataService<ImageWithText>>()

        ;
    }
}
