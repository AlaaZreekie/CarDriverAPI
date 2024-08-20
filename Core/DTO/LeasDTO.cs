using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class LeasDTO
    {
        public int carId {  get; set; }
        public string carName { get; set; }

        public int driverId {  get; set; }
        
        public string driverName { get; set; }
    }
}
