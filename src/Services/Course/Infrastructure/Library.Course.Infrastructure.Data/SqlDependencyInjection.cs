using Library.Course.Domain.Repositories;
using Library.Course.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Course.Infrastructure.Data;

public static class SqlDependencyInjection
{
    public static IServiceCollection AddSqlServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ICourseRepository), typeof(CourseRepository));

        return services;
    }
}
