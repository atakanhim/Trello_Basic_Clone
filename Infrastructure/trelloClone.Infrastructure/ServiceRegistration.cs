using trelloClone.Application.Abstractions.Token;
using trelloClone.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;


namespace trelloClone.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
