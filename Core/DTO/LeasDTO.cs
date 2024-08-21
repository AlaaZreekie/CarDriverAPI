using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class LeasDTO
    {
        public int CarId {  get; set; }
        public string? CarName { get; set; }
        public int DriverId {  get; set; }        
        public string? DriverName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
