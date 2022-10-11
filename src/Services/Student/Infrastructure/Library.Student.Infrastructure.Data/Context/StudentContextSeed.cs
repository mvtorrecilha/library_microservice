using Library.Student.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Library.Student.Infrastructure.Data.Context;

public class StudentContextSeed
{
    public async Task SeedAsync(
       StudentContext context,
       ILogger<StudentContextSeed> logger)
    {
        var policy = CreatePolicy(logger, nameof(StudentContextSeed));

        await policy.ExecuteAsync(async () =>
        {
            if (!context.Students.Any()) { await SeedDefaultStudents(context); }
        });
    }

    private static async Task SeedDefaultStudents(StudentContext context)
    {
        await context.Students.AddAsync(new StudentItem
        {
            Id = new("1673a9fd-191a-479c-a41f-3dc5611aa98e"),
            Name = "Student One",
            Email = "student_one@domain.com",
            CourseId = new("A2C6F987-D83F-4FB3-9982-68553965B421")
        });

        await context.Students.AddAsync(new StudentItem
        {
            Id = new("2c4833c4-5138-48d7-80a7-60abe82a5c6c"),
            Name = "Student Two",
            Email = "student_two@domain.com",
            CourseId = new("7ECB8A32-4452-43A0-B78A-CA1552303304")
        });

        await context.Students.AddAsync(new StudentItem
        {
            Id = new("3a2ecda5-e160-4a42-898b-8f4a73989688"),
            Name = "Student Three",
            Email = "student_three@domain.com",
            CourseId = new("7ECB8A32-4452-43A0-B78A-CA1552303304")
        });

        await context.Students.AddAsync(new StudentItem
        {
            Id = new("4f35054b-7a3a-4dce-8355-cf81b8b223d1"),
            Name = "Student Four",
            Email = "student_four@domain.com",
            CourseId = new("A2C6F987-D83F-4FB3-9982-68553965B421")
        });

        await context.SaveChangesAsync();
    }

    private static AsyncRetryPolicy CreatePolicy(
          ILogger<StudentContextSeed> logger,
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