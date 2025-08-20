using CBS.CheckManagementManagement.Helper;
using Microsoft.AspNetCore.Mvc;

namespace CBS.CheckManagementManagement.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public IActionResult ReturnFormattedResponse<T>(ServiceResponse<T> result)
        {
            ResponseObject responseObject = new ResponseObject(
                data: result.Data,
                statusCode: result.StatusCode,
                description: result.Description,
                message: result.Message,
                status: result.Status,
                errors: result.Errors
            );

            if (result.Status == "SUCCESS")
            {
                return Ok(responseObject);
            }

            return StatusCode(responseObject.StatusCode, responseObject);
        }
    }
}
