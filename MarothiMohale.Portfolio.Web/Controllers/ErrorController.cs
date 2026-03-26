using Microsoft.AspNetCore.Mvc;

namespace MarothiMohale.Portfolio.Web.Controllers;

[Route("Error")]
public class ErrorController : Controller
{
    [HttpGet("404")]
    public IActionResult NotFoundPage()
    {
        Response.StatusCode = 404;
        ViewData["Title"] = "Page Not Found";
        return View("NotFound");
    }

    [HttpGet("500")]
    public IActionResult ServerError()
    {
        Response.StatusCode = 500;
        ViewData["Title"] = "Something Went Wrong";
        return View("ServerError");
    }

    [HttpGet("{statusCode:int}")]
    public IActionResult HandleStatusCode(int statusCode)
    {
        return statusCode == 404 ? NotFoundPage() : ServerError();
    }
}
