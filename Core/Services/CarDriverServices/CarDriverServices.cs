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

public class CarDriverServices(ICarServices carService,IRepository<Car> carRepository, IRepository<Driver> driverRepository, IRepository<CarsDrivers> repository) : ICarDriverServices
{
    private readonly ICarServices _carService = carService;
    private readonly IRepository<Car> _carRepository = carRepository;
    private readonly IRepository<Driver> _driverRepository = driverRepository;
    //private readonly IRepository<CarsDrivers> _carRepository = repository;

    private readonly IRepository<CarsDrivers> _carsDriversRepository = repository;
    public LeasDTO AddDriverToCar(int carId, int driverId)
    {
        var car = new Car();
        car = _carRepository.GetById(carId);
        var driver = new Driver();
        driver = _driverRepository.GetById(driverId);
        if(car == null  ||  driver == null) { return null; }
        else
        {
            CarsDrivers carDriver = new CarsDrivers() {carId = car.Id,driverId = driver.Id };
            carDriver = _carsDriversRepository.Create(carDriver);
            LeasDTO leas = new LeasDTO();
            leas.carName = carDriver.car.CarType;
            leas.driverName = carDriver.driver.Name;
            leas.carId = carDriver.carId;
            leas.driverId = carDriver.driverId;

            return leas;
        }
       
    }
    
    public List<Pair> GetById(int carId)
    {
        var Drivers = new List<Pair>();
        //var leas = _carsDriversRepository.GetAll();
        CarDTO carDTO = _carService.GetById(carId);
        if (carDTO == null) { return null; }

        if (carDTO.drivers.Count() == 0 || carDTO.drivers == null) { return null; }
        foreach (int n in carDTO.drivers)
        {
            var driver = _driverRepository.GetById(n);
            Pair p = new Pair();
            p.Id = driver.Id;
            p.Name = driver.Name;
            Drivers.Add(p);
        }
        return Drivers;       
    }

    public List<LeasDTO> GetAllCarsWithDrivers()
    {
        List<CarsDrivers> carsDrivers = _carsDriversRepository.GetAll(c => c.car, d => d.driver).ToList();
        List<LeasDTO> leas = new List<LeasDTO>();
        for(int i = 0;i< carsDrivers.Count();i++)
        {
            LeasDTO l = new LeasDTO();
            l.carName = carsDrivers[i].car.CarType;
            l.driverName = carsDrivers[i].driver.Name;
            l.carId = carsDrivers[i].carId;
            l.driverId = carsDrivers[i].driverId;
            leas.Add(l);
        }

        return leas;
    }
}
