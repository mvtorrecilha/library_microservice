using MediatR;

namespace Library.Borrowing.Application.IntegrationEvents.Events;

public record BookBorrowingAcceptedIntegrationEvent : INotification
{
    public Guid BookId { get; }

    public Guid StudentId { get; }

    public BookBorrowingAcceptedIntegrationEvent(Guid studentId, Guid bookId)
    {
        BookId = bookId;
        StudentId = studentId;
    }
}
