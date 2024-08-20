﻿using Core.Services.DriverServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Core.Services.CarDriverServices;
using Core.DTO;
using Microsoft.AspNetCore.Authorization;



namespace Presentation.Properties.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DriverController(ICarDriverServices carDriverServices,IDriverServices driverServices, ICarServices  carServices) : Controller
    {
        private readonly IDriverServices _driverServices = driverServices;
        private readonly ICarServices _carServices = carServices;
        private ICarDriverServices _carDriverServices = carDriverServices;


        [HttpGet]
        public IActionResult GetAllDrivers()
        {
            var List = _driverServices.GetAllDrivers().ToList();
            return (List == null || List.Count == 0) ? BadRequest("There is no drivers") : Ok(List);
        }
        //[HttpPost, Authorize]
        [HttpPost, Authorize]
        public IActionResult CreateDriver([FromForm] string name)
        {
            var res = _driverServices.CreateDriver(name);
            return (res != null) ? Ok(res) : BadRequest("This driver is already exist");
        }

        //[HttpPut, Authorize]
        [HttpPut, Authorize]
        public IActionResult UpdateDriver([FromForm] int id, [FromForm] string name)
        {
            var res = _driverServices.UpdateDriver(id,name);
            return (res != null) ? Ok(res) : BadRequest("This driver does not exist");

        }

        //[HttpDelete, Authorize]
        [HttpDelete, Authorize]
        public IActionResult DeleteDriver([FromForm] int id)
        {
            var res = _driverServices.DeleteDriver(id);
            return (res != null) ? Ok("Done Deleting driver ") : BadRequest("This driver does not exist");
        }

        [HttpGet, Authorize]
        public IActionResult GetDriversByCarId(int id)
        {
            var Drivers = _carDriverServices.GetById(id);
            var car = _carServices.GetById(id);
            if(car == null) { return BadRequest("This car does not exist") ; }

            if (Drivers == null || Drivers.Count() == 0) return NotFound("There is no Driver for this car");


            return Ok(Drivers );
          
        }
        [HttpPut, Authorize]
        public IActionResult AddDriverToCar([FromForm] int carId, [FromForm] int driverId)
        {
            var car = _carServices.GetById(carId);
            if (car == null)
            {
                return BadRequest("This car does not exist");
            }
            else
            {
                var driver = _driverServices.GetById(driverId);
                var ListOfDrivers = car;
                if (driver == null ) { return BadRequest("This driver does not exist"); }
                else
                {
                    _carDriverServices.AddDriverToCar(carId, driverId);
                    return Ok("Done");
                }
            }

        }

    }
}
