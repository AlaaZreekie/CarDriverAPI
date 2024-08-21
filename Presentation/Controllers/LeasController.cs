using Core.Services.CarDriverServices;
using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class LeasController(ILeaseServices services) : ControllerBase
{
    private readonly ILeaseServices _services = services;
    [HttpGet]
    public IActionResult GetLeas()
    {
        //return Ok("done");
        var List = _services.GetAllLease().ToList();
        return List == null || List.Count() == 0 ? BadRequest("There is no Leas") : Ok((List));
    }

    [HttpPost]
    public IActionResult CreateLeas([FromForm] int carId, [FromForm] int driverId,int sYear, int sMonth, int sDay, int eYear, int eMonth, int eDay)
    {
        DateOnly startDate = new DateOnly(sYear, sMonth, sDay);
        DateOnly endDate = new DateOnly(eYear, eMonth, eDay);
        var CreatedLease = _services.CreateLease(carId, driverId, startDate, endDate);
        return CreatedLease == null  ? BadRequest("ERROR") : Ok((CreatedLease));
    }
}
