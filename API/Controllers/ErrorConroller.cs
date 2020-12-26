using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorConroller : BaseApiController
    {

    
        public IActionResult Error (int code)
        {
            return new ObjectResult (new ApiResponse(code));
        }
    }
}