using Microsoft.AspNetCore.Mvc;

namespace Library.Adapter.ResponseFormatter;

public interface IResponseFormatterResult
{
    ActionResult Format();
    ActionResult Format<T>(T body = null) where T : class;
}
