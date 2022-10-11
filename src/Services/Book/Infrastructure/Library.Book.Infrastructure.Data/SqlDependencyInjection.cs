using Library.Book.Domain.Repositories;
using Library.Book.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Book.Infrastructure.Data;

public static class SqlDependencyInjection
{
    public static IServiceCollection AddSqlServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBookRepository), typeof(BookRepository));

        return services;
    }
}
