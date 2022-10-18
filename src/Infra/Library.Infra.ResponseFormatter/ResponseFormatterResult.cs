﻿using Library.Infra.ResponseFormatter.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Infra.ResponseFormatter;

public class ResponseFormatterResult : IResponseFormatterResult
{
    private readonly INotifier _notifier;

    public ResponseFormatterResult(INotifier notifier)
    {
        _notifier = notifier;
    }

    public ActionResult Format() => Format<object>();


    public ActionResult Format<T>(T body = null) where T : class
    {
        if (_notifier.HasError)
        {
            return CreateObjectError();
        }

        return CreateObjectSuccess(body);
    }

    private ObjectResult CreateObjectError()
    {
        return new ObjectResult(_notifier.Errors)
        {
            StatusCode = (_notifier.StatusCode != default(HttpStatusCode) ?
                          _notifier.StatusCode : HttpStatusCode.BadRequest).GetHashCode()
        };
    }

    private ObjectResult CreateObjectSuccess<T>(T body = null) where T : class
    {
        return new ObjectResult(body)
        {
            StatusCode = (_notifier.StatusCode != default(HttpStatusCode) ?
                          _notifier.StatusCode : HttpStatusCode.OK).GetHashCode()
        };
    }
}
