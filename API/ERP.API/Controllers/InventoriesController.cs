using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace ERP.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[ApiController]
public class InventoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IInventoryQueries _orderQueries;
    private readonly IIdentityService _identityService;
    private readonly ILogger<InventoriesController> _logger;

    public InventoriesController(IMediator mediator, IInventoryQueries orderQueries, IIdentityService identityService, ILogger<InventoriesController> logger)
    {
        _mediator = mediator;
        _orderQueries = orderQueries;
        _identityService = identityService;
        _logger = logger;
    }
}