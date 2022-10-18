namespace Library.Domain.Core.Events;

public abstract record Message
{
    public Message()
    {
        Id = Guid.NewGuid();
        MessageType = GetType().Name;
        Timestamp = DateTime.UtcNow;
    }

    public Guid Id { get; protected set; }

    public string MessageType { get; protected set; }

    public DateTime Timestamp { get; protected set; }

    public override string ToString()
    {
        return $"Id=[{Id}] | Type=[{MessageType}]";
    }
}
