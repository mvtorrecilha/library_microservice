namespace Library.Book.Application.Behaviors;

public static class ErrorBehavior
{
    public const string
        BookNotFound = "Book not found.",
        TheBookDoesNotBelongToTheCourse = "The book does not belong to the course.",
        BookAlreadyReturned = "The book is already returned",
        BookAlreadyLent = "The book is already lent.";
}