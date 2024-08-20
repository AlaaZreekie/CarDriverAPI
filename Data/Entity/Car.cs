using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Car : TEntity //T
    {
        
        public string? CarColor { get; set; }
        public string? CarType { get; set; }
        public int NumberOfDoors { get; set; }

        public ICollection<Leas>? Leas { get; set; } 
        /*public Car()
        {

        }        
        public Car( string? color, string? type, int numDoor)
        {
            CarColor = color;
            CarType = type;
            NumberOfDoors = numDoor;
        }
        public Car(Car car)
        {
            CarColor = car.CarColor;
            Id = car.Id;
            CarType = car.CarType;
            NumberOfDoors = car.NumberOfDoors;
        }*/
    }
}