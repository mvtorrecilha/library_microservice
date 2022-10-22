using MediatR;

namespace Library.Borrowing.Application.Notifications.EventHandling;

public class ReturningRegisteredNotificationHandler : INotificationHandler<ReturningRegisteredNotification>
{
    public Task Handle(ReturningRegisteredNotification notification, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"returning history registered to book and student id: '{notification.BookId} - {notification.StudentId}'");
        });
    }
}