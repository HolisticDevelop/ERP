using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERP.Api;

[Route("/product")]
public class ProductsCommandsApi : Controller
{
    private readonly ProductsApplicationService _applicationService;

    public ProductsCommandsApi(
        ProductsApplicationService applicationService)
        => _applicationService = applicationService;

    [HttpPost]
    public async Task<IActionResult> Post(
        Sales.Products.V1.Create request)
    {
        await _applicationService.Handle(request);

        Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

        return Ok();
    }
}