using Core.Services.CarDriverServices;
using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Properties.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class LeasController(ICarDriverServices services) : ControllerBase
{
    private readonly ICarDriverServices _services = services;
    [HttpGet]
    public IActionResult GetLeas()
    {
        //return Ok("done");
        var List = _services.GetAllCarsWithDrivers().ToList();
        return (List == null || List.Count() == 0) ? BadRequest("There is no Leas") : Ok(List);
    }
}
