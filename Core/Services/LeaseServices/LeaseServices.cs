using Core.DTO;
using Data.Cars;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services.DriverServices;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.CarDriverServices;

public class LeaseServices(ICarServices carService,IRepository<Car> carRepository, IRepository<Driver> driverRepository, IRepository<CarDriver> repository) : ILeaseServices
{
    private readonly ICarServices _carService = carService;
    private readonly IRepository<Car> _carRepository = carRepository;
    private readonly IRepository<Driver> _driverRepository = driverRepository;
    //private readonly IRepository<CarsDrivers> _carRepository = repository;

    private readonly IRepository<CarDriver> _carsDriversRepository = repository;
    public LeasDTO? AddDriverToCar(int carId, int driverId)
    {
        var car = new Car();
        car = _carRepository.GetById(carId);
        var driver = new Driver();
        driver = _driverRepository.GetById(driverId);
        if(car == null  ||  driver == null) { return null; }
        else
        {
            CarDriver carDriver = new() {CarId = car.Id,DriverId = driver.Id };
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

    public List<LeasDTO> GetAllLease()
    {
        List<CarDriver> carsDrivers = [.. _carsDriversRepository.GetAll(c => c.Car, d => d.Driver)];
        List<LeasDTO> leas = [];
        if(carsDrivers == null) { return leas; }

        for(int i = 0;i< carsDrivers.Count; i++)
        {
            LeasDTO l = new()
            {
                CarName = carsDrivers[i].Car.CarType,
                DriverName = carsDrivers[i].Driver.Name,
                CarId = carsDrivers[i].CarId,
                DriverId = carsDrivers[i].DriverId,
                StartDate = carsDrivers[i].StartDate,
                EndDate = carsDrivers[i].EndDate,                
            };
            leas.Add(l);
        }

        return leas;
    }

    public LeasDTO? CreateLease(int carId, int driverId, DateOnly StartDate, DateOnly EndDate)
    {
        var car = new Car();
        car = _carRepository.GetById(carId);
        var driver = new Driver();
        driver = _driverRepository.GetById(driverId);
        var check = IsLeaseOverlapping(carId, StartDate, EndDate);
        if (car == null || driver == null) { throw new Exception("Wrong Details"); }
        else if (!check) { throw new Exception("Wrong Details"); }
        else
        {
            CarDriver carDriver = new() { CarId = car.Id, DriverId = driver.Id, StartDate = StartDate, EndDate = EndDate };
            var rescarDriver = _carsDriversRepository.Create(carDriver);
            var leasDTO = new LeasDTO();
            
            if (rescarDriver == null || rescarDriver.StartDate == null|| rescarDriver.EndDate == null) { return null; }

            else {leasDTO.CarName = rescarDriver.Car.CarType;
            leasDTO.DriverName = rescarDriver.Driver.Name;
            leasDTO.CarId = rescarDriver.CarId;
            leasDTO.DriverId = rescarDriver.DriverId;
            leasDTO.StartDate = rescarDriver.StartDate;
            leasDTO.EndDate = rescarDriver.EndDate; 
            }
            
            
            return leasDTO;
        }
    }

    public bool IsLeaseOverlapping(int carId, DateOnly startDate, DateOnly endDate)
    {
        if ((endDate <= startDate)) {  return false; }
         var List =  _carsDriversRepository.GetAll();
        if(List == null) { return true; }
        var leaseList = List.ToList();        
        if (leaseList == null || leaseList.Count == 0) { return true; }
        foreach(CarDriver l in leaseList)
        {
            if (l.CarId == carId && (l.StartDate == startDate || l.EndDate == endDate))
            {
                return false;
            }
            if (l.CarId == carId && ((startDate >= l.StartDate && startDate <= l.EndDate) || (endDate <= l.EndDate && endDate >= l.StartDate)))
            {
                return false;
            }

        }
        return true;
    }

    /*public async Task<CarDriver> CreateLeaseAsync(LeasDTO lease)
    {

        if (await IsLeaseOverlapping(lease.CarId, lease.StartDate, lease.EndDate))
        {
            throw new InvalidOperationException("Lease period overlaps with an existing lease.");
        }

        _context.Leases.Add(lease);
        await _context.SaveChangesAsync();

        return lease;
    }*/
}
