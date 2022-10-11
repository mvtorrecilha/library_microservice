using Library.Student.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Student.API.Configuration;

public static class SqlConfiguration
{
    public static IServiceCollection AddSqlData(this IServiceCollection services, IConfiguration configuration)
    {
        services
                .AddDbContext<StudentContext>(options =>
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
