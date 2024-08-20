using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTO;
using Data.Entity;

namespace Core.Services
{
    public interface ICarServices
    {
        public List<CarDTO>? GetAllCars();
        public CarDTO? GetById(int id);
        public CarDTO CreateCar(string? color, string? type, int numDoor);
        public CarDTO UpdateCar(int id, string? color, string? type, int numDoor);
        public CarDTO? DeleteCar(int id);      


    }
}