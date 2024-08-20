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


public class CarServices(/*UserManager<IdentityUser> userManeger,*/ IRepository<Car> carrepository, IRepository<Driver> driverRepository, IRepository<Leas> repository) : ICarServices   //CarServices   IRepository     Repository
{
    //private readonly UserManager<IdentityUser> _userManeger = userManeger;
    private readonly IRepository<Car> _carRepository = carrepository;
    private readonly IRepository<Driver> _driverRepository = driverRepository;
    private readonly IRepository<Leas> _repository = repository;


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
            CarDTO carDTO = new()
            {
                Id = c.Id,
                DoorsNum = c.NumberOfDoors,
                Color = c.CarColor,
                Type = c.CarType,
                Drivers = []
            };
            if ( c.Leas == null || c.Leas.Count == 0 )
            {
                carsList.Add(carDTO);
            }
            else
            {
                foreach (Leas cd in c.Leas)
                {                    
                    carDTO.Drivers.Add(cd.DriverId);
                }            
                carsList.Add(carDTO);
            }                
        }
        return carsList;
    }
     public CarDTO? GetById(int id)
    {
        CarDTO carDTO = new();
        var car = _carRepository.GetById(id, c => c.Leas);
        if (car == null) { return null; }
        if(car.Leas == null || car.Leas.Count() == 0) { return carDTO; }
        carDTO = new() { Id = car.Id, Color = car.CarColor, Type = car.CarType, DoorsNum = car.NumberOfDoors, Drivers = [] };


        foreach (Leas c in car.Leas)
        {
            var d = new Driver();
            //Driver d = _driverRepository.GetById(id);            
            d = _driverRepository.GetById(c.DriverId);            
            carDTO.Drivers.Add(c.DriverId);
        }
        return carDTO;
    }

    public CarDTO? UpdateCar(int id,string? color, string? type, int numDoor)
    {
        var car = _carRepository.GetById(id);
             
        if (car == null)
        {
            return null;
        }
        else
        {
            var carDTO = GetById(id);
            if (carDTO == null) { return null; }
            car.CarColor = color;
            car.CarType = type;
            car.NumberOfDoors = numDoor;
            car = _carRepository.Update(car);
            if(car == null) { return null; }
            carDTO.Id = car.Id;
            carDTO.Type = car.CarType;
            carDTO.DoorsNum = car.NumberOfDoors;
            carDTO.Color = car.CarColor;
            return carDTO;
        }
        
    }

    public CarDTO? DeleteCar(int id)
    {
        var car = _carRepository.GetById(id);
        if (car != null)
        {

            CarDTO carDTO = new() { DoorsNum = car.NumberOfDoors, Id = car.Id, Type = car.CarType };

            _carRepository.Delete(car );
            
            return carDTO   ;            
        }
        else
        {
            return null;
        }
                
    }

    
    public CarDTO? CreateCar(string? color, string? type, int numDoor)
    {
        var car = new Car()
        {
            CarColor = color,
            CarType = type,
            NumberOfDoors = numDoor
        };
        car = _carRepository.Create(car);
        if(car == null) { return null; }
        CarDTO carDTO = new() { Id =  car.Id,Color= car.CarColor,Type = car.CarType,DoorsNum= car.NumberOfDoors};
        return carDTO;
    }

    
}