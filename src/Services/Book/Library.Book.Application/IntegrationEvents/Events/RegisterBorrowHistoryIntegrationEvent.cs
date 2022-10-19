using Library.Domain.Core.Events;

namespace Library.Book.Application.IntegrationEvents.Events;

public record RegisterBorrowHistoryIntegrationEvent : IntegrationEvent
{
    public Guid StudentId { get; }

    public Guid BookId { get; }

    public RegisterBorrowHistoryIntegrationEvent(Guid studentId, Guid bookId)
    {
        StudentId = studentId;
        BookId = bookId;
    }
}
