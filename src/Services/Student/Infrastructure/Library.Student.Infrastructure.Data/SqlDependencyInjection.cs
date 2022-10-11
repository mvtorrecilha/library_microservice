using Library.Student.Domain.Repositories;
using Library.Student.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Student.Infrastructure.Data;

public static class SqlDependencyInjection
{
    public static IServiceCollection AddSqlServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));

        return services;
    }
}
