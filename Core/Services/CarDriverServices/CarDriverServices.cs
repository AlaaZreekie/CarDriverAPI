using Core.DTO;
using Data.Cars;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services.DriverServices;

namespace Core.Services.CarDriverServices;

public class CarDriverServices(ICarServices carService,IRepository<Car> carRepository, IRepository<Driver> driverRepository, IRepository<Leas> repository) : ICarDriverServices
{
    private readonly ICarServices _carService = carService;
    private readonly IRepository<Car> _carRepository = carRepository;
    private readonly IRepository<Driver> _driverRepository = driverRepository;
    //private readonly IRepository<CarsDrivers> _carRepository = repository;

    private readonly IRepository<Leas> _carsDriversRepository = repository;
    public LeasDTO? AddDriverToCar(int carId, int driverId)
    {
        var car = new Car();
        car = _carRepository.GetById(carId);
        var driver = new Driver();
        driver = _driverRepository.GetById(driverId);
        if(car == null  ||  driver == null) { return null; }
        else
        {
            Leas carDriver = new() {CarId = car.Id,DriverId = driver.Id };
            var rescarDriver = _carsDriversRepository.Create(carDriver);
            LeasDTO leas = new LeasDTO();
            if(rescarDriver == null) { return null; }
            leas.CarName = rescarDriver.Car.CarType;
            leas.DriverName = rescarDriver.Driver.Name;
            leas.CarId = rescarDriver.CarId;
            leas.DriverId = rescarDriver.DriverId;

            return leas;
        }
    }
    
    public List<Pair>? GetById(int carId)
    {
        var Drivers = new List<Pair>();
        //var leas = _carsDriversRepository.GetAll();
        var carDTO = _carService.GetById(carId);
        if (carDTO == null) { return null; }

        if (carDTO.Drivers.Count() == 0 || carDTO.Drivers == null) { return null; }
        foreach (int n in carDTO.Drivers)
        {
            var driver = _driverRepository.GetById(n);
            if (driver == null) { continue; }
            var p = new Pair
            {
                Id = driver.Id,
                Name = driver.Name
            };
            Drivers.Add(p);
        }
        return Drivers;       
    }

    public List<LeasDTO> GetAllCarsWithDrivers()
    {
        List<Leas> carsDrivers = [.. _carsDriversRepository.GetAll(c => c.Car, d => d.Driver)];
        List<LeasDTO> leas = [];
        if(carsDrivers == null) { return leas; }

        for(int i = 0;i< carsDrivers.Count; i++)
        {
            LeasDTO l = new()
            {
                CarName = carsDrivers[i].Car.CarType,
                DriverName = carsDrivers[i].Driver.Name,
                CarId = carsDrivers[i].CarId,
                DriverId = carsDrivers[i].DriverId
            };
            leas.Add(l);
        }

        return leas;
    }
}
