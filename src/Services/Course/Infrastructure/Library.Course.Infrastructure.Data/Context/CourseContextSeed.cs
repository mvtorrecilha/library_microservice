using Library.Course.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Library.Course.Infrastructure.Data.Context;

public class CourseContextSeed
{
    public async Task SeedAsync(
       CourseContext context,
       ILogger<CourseContextSeed> logger)
    {
        var policy = CreatePolicy(logger, nameof(CourseContextSeed));

        await policy.ExecuteAsync(async () =>
        {
            if (!context.Courses.Any()) { await SeedDefaultCourses(context); }
        });
    }

    private static async Task SeedDefaultCourses(CourseContext context)
    {
        await context.Courses.AddAsync(new CourseItem
        {
            Id = new("A2C6F987-D83F-4FB3-9982-68553965B421"),
            Name = "Systems Analysis"
        });

        await context.Courses.AddAsync(new CourseItem
        {
            Id = new("7ECB8A32-4452-43A0-B78A-CA1552303304"),
            Name = "Civil Engineering"
        });

        await context.SaveChangesAsync();
    }

    private static AsyncRetryPolicy CreatePolicy(
          ILogger<CourseContextSeed> logger,
          string prefix,
          int retries = 3)
    {
        return Policy.Handle<SqlException>().
            WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogWarning(
                        exception,
                        "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}",
                        prefix,
                        exception.GetType().Name,
                        exception.Message,
                        retry,
                        retries);
                }
            );
    }
}
