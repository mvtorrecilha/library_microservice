using Grpc.Core;
using System.Net;

namespace Library.Infra.ResponseFormatter.Common;

public class GrpcCommon
{

    public static RpcException GetRpcException(HttpStatusCode StatusCode, List<Notification> Errors)
    {
        var mainError = "";
        foreach (var error in Errors)
        {
            mainError = String.Concat(mainError, error.Message, "\n");
        }

        var status = GetStatus(StatusCode, mainError);
        return new RpcException(status);
    }

    private static Status GetStatus(HttpStatusCode httpStatusCode, string mainErrorReason)
    {
        var statusCode = new StatusCode();

        switch (httpStatusCode)
        {
            case HttpStatusCode.BadRequest:
                statusCode = StatusCode.InvalidArgument;
                break;
            case HttpStatusCode.NotFound:
                statusCode = StatusCode.NotFound;
                break;
            case HttpStatusCode.Forbidden:
                statusCode = StatusCode.PermissionDenied;
                break;
            case HttpStatusCode.Conflict:
                statusCode = StatusCode.AlreadyExists;
                break;

            default:
                statusCode = StatusCode.Internal;
                break;
        }
        return new Status(statusCode, mainErrorReason);
    }
}
