using Microsoft.AspNetCore.Mvc;

namespace Brewery.Api.Controllers;

[ApiController]
[Route("/[controller]")]
public class BaseController : ControllerBase
{
    public ActionResult<TModel> OkOrNotFound<TModel>(TModel model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}