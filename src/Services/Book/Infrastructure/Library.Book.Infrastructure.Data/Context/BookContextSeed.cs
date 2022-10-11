using Library.Book.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Library.Book.Infrastructure.Data.Context;

public class BookContextSeed
{
    public async Task SeedAsync(
       BookContext context,
       ILogger<BookContextSeed> logger)
    {
        var policy = CreatePolicy(logger, nameof(BookContextSeed));

        await policy.ExecuteAsync(async () =>
        {
            if (!context.Categories.Any()) { await SeedDefaultCategories(context); }
            if (!context.Books.Any()) { await SeedDefaultBooks(context); }
        });
    }

    private static async Task SeedDefaultCategories(BookContext context)
    {
        await context.Categories.AddAsync(new Category
        {
            Id = new("1173a9fd-191a-479c-a41f-3dc5611aa98e"),
            Name = "IT"
        });

        await context.Categories.AddAsync(new Category
        {
            Id = new("2273a9fd-191a-479c-a41f-3dc5611aa98e"),
            Name = "Civil Engineering"
        });

        await context.SaveChangesAsync();
    }

    private static async Task SeedDefaultBooks(BookContext context)
    {
        await context.Books.AddAsync(new BookItem
        {
            Title = "Clean Code",
            Author = "Uncle Bob",
            Pages = 429,
            Publisher = "Atlas",
            CategoryId = new("1173a9fd-191a-479c-a41f-3dc5611aa98e"),
            CourseId = new ("A2C6F987-D83F-4FB3-9982-68553965B421"),
            IsAvailable = true,
            ImageUrl = "clean_code"
        });

        await context.Books.AddAsync(new BookItem
        {
            Title = "Pragmatic Programmer 2",
            Author = "Andrew Hunt",
            Pages = 352,
            Publisher = "Addison-Wesley Professional",
            CategoryId = new("1173a9fd-191a-479c-a41f-3dc5611aa98e"),
            CourseId = new("A2C6F987-D83F-4FB3-9982-68553965B421"),
            IsAvailable = true,
            ImageUrl = "Pragmatic"
        });

        await context.Books.AddAsync(new BookItem
        {
            Title = "O edifício até sua cobertura",
            Author = "Hélio Alves de Azeredo",
            Pages = 193,
            Publisher = "Blucher",
            CategoryId = new("2273a9fd-191a-479c-a41f-3dc5611aa98e"),
            CourseId = new("7ECB8A32-4452-43A0-B78A-CA1552303304"),
            IsAvailable = true,
            ImageUrl = "edificio"
        });

        await context.Books.AddAsync(new BookItem
        {
            Title = "Concreto armado: eu te amo",
            Author = "Manoel Henrique Campos",
            Pages = 652,
            Publisher = "Blucher",
            CategoryId = new("2273a9fd-191a-479c-a41f-3dc5611aa98e"),
            CourseId = new("7ECB8A32-4452-43A0-B78A-CA1552303304"),
            IsAvailable = true,
            ImageUrl = "concreto_armado"
        });

        await context.SaveChangesAsync();
    }

    private static AsyncRetryPolicy CreatePolicy(
          ILogger<BookContextSeed> logger,
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
