using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CarController(ICarServices carServices, UserManager<IdentityUser> userManeger) : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager = userManeger;
    private readonly ICarServices _carServices = carServices;


    [HttpGet]
    public IActionResult GetCars()
    {
        var List = _carServices.GetAllCars();
        return List == null || List.Count() == 0 ? BadRequest("There is no Cars") : Ok(List);
    }
    [HttpPost, Authorize()]
    //[HttpPost]
    public IActionResult CreateCar([FromBody] CarDTO car )
    {
        var res = _carServices.CreateCar(car.Color, car.Type, car.DoorsNum);
        return res != null ? Ok(res) : BadRequest("This car is already exist");
    }
    [HttpPut, Authorize()]
    public IActionResult UpdateCar([FromForm] int id, [FromForm] string? color, [FromForm] string? type, [FromForm] int numDoor)
    {
        var res = _carServices.UpdateCar(id, color, type, numDoor);
        return res != null ? Ok(res) : BadRequest("This car does not exist");

    }
    [HttpDelete, Authorize()]
    public IActionResult DeleteCar([FromForm] int id)
    {
        var res = _carServices.DeleteCar(id);
        return res != null ? Ok("Done Deleting Car ") : BadRequest("This car does not exist");
    }
}