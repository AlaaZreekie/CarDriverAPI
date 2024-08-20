using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Core.DTO;
using Data.Cars;
using Data.Entity;
using Microsoft.AspNetCore.Identity;

namespace Core.Services;


public class CarServices(/*UserManager<IdentityUser> userManeger,*/ IRepository<Car> carrepository, IRepository<Driver> driverRepository, IRepository<CarsDrivers> repository) : ICarServices   //CarServices   IRepository     Repository
{
    //private readonly UserManager<IdentityUser> _userManeger = userManeger;
    private readonly IRepository<Car> _carRepository = carrepository;
    private readonly IRepository<Driver> _driverRepository = driverRepository;
    private readonly IRepository<CarsDrivers> _repository = repository;


    public List<CarDTO>? GetAllCars()
    {        
        List<CarDTO> carsList = new List<CarDTO>();
        
        var cars = _carRepository.GetAll(c => c.Leas );
        if (cars == null || cars.Count() == 0) 
        {        
            return carsList;
        }
        foreach (Car c in cars)
        {
            CarDTO carDTO = new();
            carDTO.Id = c.Id;
            carDTO.doorsNum = c.NumberOfDoors;
            carDTO.color = c.CarColor;
            carDTO.type = c.CarType;
            carDTO.drivers = [];
            if (c.Leas.Count == 0 || c.Leas == null)
            {
                carsList.Add(carDTO);
            }
            else
            {
                foreach (CarsDrivers cd in c.Leas)
                {                    
                    carDTO.drivers.Add(cd.driverId);
                }            
                carsList.Add(carDTO);
            }                
        }
        return carsList;
    }
     public CarDTO? GetById(int id)
    {
        CarDTO carDTO = new();
        Car car = _carRepository.GetById(id, c => c.Leas);
        if (car == null) { return null; }
        if(car.Leas == null || car.Leas.Count() == 0) { return carDTO; }
        carDTO = new() { Id = car.Id, color = car.CarColor, type = car.CarType, doorsNum = car.NumberOfDoors, drivers = [] };


        foreach (CarsDrivers c in car.Leas)
        {
            Driver d = new Driver();
            //Driver d = _driverRepository.GetById(id);            
            d = _driverRepository.GetById(c.driverId);            
            carDTO.drivers.Add(c.driverId);
        }
        return carDTO;
    }

    public CarDTO UpdateCar(int id,string? color, string? type, int numDoor)
    {
        Car car = _carRepository.GetById(id);
             
        if (car == null)
        {
            return null;
        }
        else
        {
            CarDTO carDTO = GetById(id);
            car.CarColor = color;
            car.CarType = type;
            car.NumberOfDoors = numDoor;
            car = _carRepository.Update(car);           
            carDTO.Id = car.Id;
            carDTO.type = car.CarType;
            carDTO.doorsNum = car.NumberOfDoors;
            carDTO.color = car.CarColor;
            return carDTO;
        }
        
    }

    public CarDTO DeleteCar(int id)
    {
        Car car = _carRepository.GetById(id);
        if (car != null)
        {

            CarDTO carDTO = new() { doorsNum = car.NumberOfDoors, Id = car.Id, type = car.CarType };

            _carRepository.Delete(car );
            
            return carDTO   ;            
        }
        else
        {
            return null;
        }
                
    }

    
    public CarDTO CreateCar(string? color, string? type, int numDoor)
    {
        Car car = new Car();
        car.CarColor = color;
         car.CarType = type;
         car.NumberOfDoors = numDoor;
        car = _carRepository.Create(car);
        CarDTO carDTO = new() { Id =  car.Id,color= car.CarColor,type = car.CarType,doorsNum= car.NumberOfDoors};
        return carDTO;
    }

    
}