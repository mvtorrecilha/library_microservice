using MediatR;

namespace Library.Borrowing.Application.Notifications.EventHandling;

public class BorrowingRegisteredNotificationHandler : INotificationHandler<BorrowingRegisteredNotification>
{
    public Task Handle(BorrowingRegisteredNotification notification, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"Borrowing history registered to book and student id: '{notification.BookId} - {notification.StudentId}'");
        });
    }
}