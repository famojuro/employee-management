using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, requested resource not found";
                    _logger.LogWarning($"404 Error Occured, Path = {statusCodeResult.OriginalPath}" +
                                       $" and QueryString = {statusCodeResult.OriginalQueryString}");
                    break;
            }
            
            return View("NotFound");
        }

        [Route("error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            
            _logger.LogError($"the path {exceptionDetails.Path} threw an exception " + 
                             $"{exceptionDetails.Error}");
            
            return View("Error");

        }
    }
}