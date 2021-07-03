using Core.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;

namespace ApiMentoria.Util.Mappers
{
    public static class RepositoryMapper
    {
        public static void MapRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
