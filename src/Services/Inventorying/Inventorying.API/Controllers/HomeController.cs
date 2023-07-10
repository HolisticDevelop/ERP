namespace ERP.Services.Inventorying.API.Controllers;

public class HomeController : ControllerBase
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        return new RedirectResult("~/swagger");
    }
}