﻿using Core.DTO;
using Data.Cars;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.DriverServices;

public class DriverServices(IRepository<Driver> driverRepository, IRepository<Car> carRepository, IRepository<CarsDrivers> repository) : IDriverServices
{

    private readonly IRepository<Car> _carRepository = carRepository;
    private readonly IRepository<Driver> _driverRepository = driverRepository;
    private readonly IRepository<CarsDrivers> _repository = repository;


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

            foreach (CarsDrivers cd in d.Leas)
            {
                driverDTO.Cars.Add(cd.carId);
            }

            driverList.Add(driverDTO);
        }
        return driverList;
    }
    public DriverDTO? GetById(int id)
    {
        Driver driver = _driverRepository.GetById(id, c => c.Leas);
        if (driver == null) {  return null; }
        DriverDTO driverDTO = new();
        if (driver.Leas == null || driver.Leas.Count() == 0) { return driverDTO; }
         driverDTO = new() { Id = driver.Id, Name = driver.Name, Cars = [] };

        foreach (CarsDrivers d in driver.Leas)
        {                       
            driverDTO.Cars.Add(d.carId);
        }
        return driverDTO;
    }

    public DriverDTO UpdateDriver(int id,string name)
    {
        Driver driver = _driverRepository.GetById(id);

        if (driver == null)
        {
            return null;
        }
        else
        {
            DriverDTO driverDTO = GetById(id);
            driver.Id = id;
            driver.Name = name;            
            driver = _driverRepository.Update(driver);
            driverDTO.Id = driver.Id;
            driverDTO.Name = driver.Name;
            
            return driverDTO;
        }

    }

    public DriverDTO DeleteDriver(int id)
    {
        Driver driver = _driverRepository.GetById(id);
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
        Driver driver = new Driver();
        driver.Name = name;

        driver = _driverRepository.Create(driver);
        DriverDTO driverDTO = new() { Id = driver.Id, Name = driver.Name };
        return driverDTO;
    }
}