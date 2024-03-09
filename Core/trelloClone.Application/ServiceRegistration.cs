
using Microsoft.Extensions.DependencyInjection;
using trelloClone.Application.Mappings;

namespace trelloClone.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationTrelloProfile));


        }
    }
}