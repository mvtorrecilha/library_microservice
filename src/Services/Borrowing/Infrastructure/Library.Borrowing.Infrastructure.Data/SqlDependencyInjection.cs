using Library.Borrowing.Domain.Repositories;
using Library.Borrowing.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Borrowing.Infrastructure.Data;

public static class SqlDependencyInjection
{
    public static IServiceCollection AddSqlServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBorrowingRepository), typeof(BorrowingRepository));

        return services;
    }
}
