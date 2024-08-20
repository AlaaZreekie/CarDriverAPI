using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class CarsDrivers : TEntity
    {

        public required int carId { get; set; }
        public required int driverId { get; set; }
        public Car car { get; set; }
        public Driver driver { get; set; }
        


    }
}
