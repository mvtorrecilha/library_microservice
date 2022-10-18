using Microsoft.AspNetCore.Mvc;

namespace Library.Infra.ResponseFormatter;

public interface IResponseFormatterResult
{
    ActionResult Format();
    ActionResult Format<T>(T body = null) where T : class;
}
