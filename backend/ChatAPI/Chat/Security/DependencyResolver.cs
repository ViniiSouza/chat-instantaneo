using Chat.Application.Services;
using Chat.Domain.Interfaces.Repositories;
using Chat.Domain.Interfaces.Services;
using Chat.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Security
{
    public static class DependencyResolver
    {
        public static void AddDependencies(this IServiceCollection service)
        {
            // repositories
            service.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            service.AddTransient(typeof(IUserRepository), typeof(UserRepository));

            // app services
            service.AddTransient(typeof(IBaseAppService<,>), typeof(BaseAppService<,>));
            service.AddTransient(typeof(IUserAppService), typeof(UserAppService));
        }
    }
}
