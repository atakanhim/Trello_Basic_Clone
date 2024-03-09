using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using trelloClone.Application.Repositories;
using trelloClone.Persistence.Repositories;
using trelloClone.Application.Abstractions.Services;
using trelloClone.Persistence.Services;
using trelloClone.Persistence.Context;
using trelloClone.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using trelloClone.Domain.Entities.Identity;

namespace trelloClone.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            // context
            //microsoft.extension.configrutaion added json dosyayı okucaz

            services.AddDbContext<TrelloCloneDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            services.AddIdentity<AppUser,AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<TrelloCloneDbContext>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IListService, ListService>();
            services.AddScoped<ICardService, CardService>();




            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IListRepository, ListRepository>();
         


        }

    }
}
