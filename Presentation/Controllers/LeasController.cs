using Core.Services.CarDriverServices;
using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using Core.DTO;

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
    public IActionResult CreateLeas([FromBody] LeasDTO leas)
    {
        var CreatedLease = _services.CreateLease(leas);
        return CreatedLease == null  ? BadRequest("ERROR") : Ok((CreatedLease));
    }
}
