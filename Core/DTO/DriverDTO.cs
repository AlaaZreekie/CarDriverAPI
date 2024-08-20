using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class DriverDTO
    {
        public string Name { get; set; }
        public int id { get; set; }
        public List<int> cars { get; set; } = [];
        public DriverDTO() { }


    }
}
