using Library.Borrowing.Domain.Repositories;
using Library.Borrowing.Infrastructure.Data.Context;
using Library.Borrowing.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Borrowing.Infrastructure.Data;

public static class SqlDependencyInjection
{
    public static IServiceCollection AddSqlServices(this IServiceCollection services)
    {
        services.AddScoped<IBorrowingRepository, BorrowingRepository>(sp =>
        {
            var context = sp.GetRequiredService<BorrowContext>();
            return new BorrowingRepository(context);
        });
        return services;
    }
}
