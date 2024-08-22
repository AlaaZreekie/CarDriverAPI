using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConsoleApp
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Color { get; set; }
        public int? DoorsNum { get; set; }
        public List<int> Drivers { get; set; } = [];
    }
}
