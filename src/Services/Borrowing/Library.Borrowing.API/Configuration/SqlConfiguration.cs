using Library.Borrowing.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Borrowing.API.Configuration;

public static class SqlConfiguration
{
    public static IServiceCollection AddSqlData(this IServiceCollection services, IConfiguration configuration)
    {
        services
                .AddDbContext<BorrowContext>(options =>
                {
                    options.UseSqlServer(
                        configuration["ConnectionStrings:DefaultConnection"],
                        o =>
                        {
                            o.MigrationsHistoryTable(tableName: "__ApplicationMigrationsHistory");
                            o.CommandTimeout(30);
                        });
                });

        return services;
    }
}
