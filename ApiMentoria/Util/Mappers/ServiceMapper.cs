using Core.Abstractions.Services;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApiMentoria.Util.Mappers
{
    public static class ServiceMapper
    {
        public static void MapServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}