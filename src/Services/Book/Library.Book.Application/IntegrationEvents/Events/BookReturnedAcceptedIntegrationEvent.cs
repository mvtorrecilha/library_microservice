using Library.Domain.Core.Events;

namespace Library.Book.Application.IntegrationEvents.Events;

public record BookReturnedAcceptedIntegrationEvent : IntegrationEvent
{
    public Guid StudentId { get; }

    public Guid BookId { get; }

    public BookReturnedAcceptedIntegrationEvent(Guid studentId, Guid bookId)
    {
        StudentId = studentId;
        BookId = bookId;
    }
}
