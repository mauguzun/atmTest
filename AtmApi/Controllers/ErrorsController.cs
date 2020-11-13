using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AtmApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public IActionResult Error() => BadRequest(JsonSerializer.Serialize(HttpContext.Features.Get<IExceptionHandlerFeature>().Error.Message));
    }
}
