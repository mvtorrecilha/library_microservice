namespace Library.Adapter.EventBus.Extensions;

public class EventBusOptions
{
    public const string SectionName = "EventBus";

    public bool ServiceBusEnabled { get; set; }

    public int RetryCount { get; set; }
}
