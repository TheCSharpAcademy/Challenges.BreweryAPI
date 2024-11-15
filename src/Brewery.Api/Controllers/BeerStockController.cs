using Microsoft.AspNetCore.Mvc;

namespace Brewery.Api.Controllers;

public class BeerStockController : BaseController
{
    [HttpPost]
    public async Task<ActionResult> Post(AddBeerStock addBeerStock)
    
    
}