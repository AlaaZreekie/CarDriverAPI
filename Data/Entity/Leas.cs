using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Leas : TEntity
    {

        public required int CarId { get; set; }
        public required int DriverId { get; set; }
        public Car Car { get; set; }
        public Driver Driver { get; set; }
        


    }
}
