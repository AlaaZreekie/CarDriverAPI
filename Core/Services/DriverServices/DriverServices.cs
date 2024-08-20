﻿using Core.DTO;
using Data.Cars;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.DriverServices;

public class DriverServices(IRepository<Driver> driverRepository, IRepository<Car> carRepository, IRepository<Leas> repository) : IDriverServices
{

    private readonly IRepository<Car> _carRepository = carRepository;
    private readonly IRepository<Driver> _driverRepository = driverRepository;
    private readonly IRepository<Leas> _repository = repository;


    public List<DriverDTO>? GetAllDrivers()
    {
        List<DriverDTO> driverList = new List<DriverDTO>();

        var drivers = _driverRepository.GetAll(c => c.Leas);
        if (drivers == null || drivers.Count() == 0)
        {           
            return driverList;

        }
        foreach (Driver d in drivers)
        {
            DriverDTO driverDTO = new();
            driverDTO.Id = d.Id;
            driverDTO.Name = d.Name;
            driverDTO.Cars = [];

            foreach (Leas cd in d.Leas)
            {
                driverDTO.Cars.Add(cd.CarId);
            }

            driverList.Add(driverDTO);
        }
        return driverList;
    }
    public DriverDTO? GetById(int id)
    {
        var driver = _driverRepository.GetById(id, c => c.Leas);
        if (driver == null) {  return null; }
        DriverDTO driverDTO = new();
        if (driver.Leas == null || driver.Leas.Count() == 0) { return driverDTO; }
         driverDTO = new() { Id = driver.Id, Name = driver.Name, Cars = [] };

        foreach (Leas d in driver.Leas)
        {                       
            driverDTO.Cars.Add(d.CarId);
        }
        return driverDTO;
    }

    public DriverDTO? UpdateDriver(int id,string name)
    {
        var driver = _driverRepository.GetById(id);

        if (driver == null)
        {
            return null;
        }
        else
        {
            var driverDTO = GetById(id);
            driver.Id = id;
            driver.Name = name;            
            driver = _driverRepository.Update(driver);
            driverDTO = new DriverDTO();
            if(driver ==  null) { return null; }
            driverDTO.Id = driver.Id;            
            driverDTO.Name = driver.Name;            
            return driverDTO;
        }

    }

    public DriverDTO? DeleteDriver(int id)
    {
        var driver = _driverRepository.GetById(id);
        if (driver != null)
        {

            DriverDTO driverDTO = new() { Id = driver.Id, Name = driver.Name };

            _driverRepository.Delete(driver);

            return driverDTO;
        }
        else
        {
            return null;
        }

    }


    public DriverDTO CreateDriver(string? name)
    {
        var driver = new Driver();
        driver.Name = name;

        driver = _driverRepository.Create(driver);
        if (driver == null) { throw new Exception("ERROR CREATING"); }
        DriverDTO driverDTO = new() { Id = driver.Id, Name = driver.Name };
        return driverDTO;
    }
}