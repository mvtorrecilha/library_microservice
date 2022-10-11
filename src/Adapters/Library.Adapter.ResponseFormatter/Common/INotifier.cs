using System.Net;

namespace Library.Adapter.ResponseFormatter.Common;

public interface INotifier
{
    public bool HasError { get; }
    public HttpStatusCode StatusCode { get; }
    public List<Notification> Warnings { get; }
    public List<Notification> Errors { get; }
    void SetStatuCode(HttpStatusCode statusCode);
    void AddWarning(string code, string message, object value);
    void AddError(string code, string message, object value);
}

