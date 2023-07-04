using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers;

public class HomeController : ControllerBase
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        return new RedirectResult("~/swagger");
    }
}