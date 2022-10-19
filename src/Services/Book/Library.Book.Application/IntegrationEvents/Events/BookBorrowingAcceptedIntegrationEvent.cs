using Library.Domain.Core.Events;

namespace Library.Book.Application.IntegrationEvents.Events;

public record BookBorrowingAcceptedIntegrationEvent : IntegrationEvent
{
    public Guid BookId { get; }

    public Guid StudentId { get; }

    public BookBorrowingAcceptedIntegrationEvent(Guid studentId, Guid bookId)
    {
        BookId = bookId;
        StudentId = studentId;
    }
}
