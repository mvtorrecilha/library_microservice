using Library.Course.Domain.Repositories;
using Library.Course.Infrastructure.Data.Context;
using Library.Course.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Course.Infrastructure.Data;

public static class SqlDependencyInjection
{
    public static IServiceCollection AddSqlServices(this IServiceCollection services)
    {
        services.AddScoped<ICourseRepository, CourseRepository>(sp =>
        {
            var context = sp.GetRequiredService<CourseContext>();
            return new CourseRepository(context);
        });
        return services;
    }
}
