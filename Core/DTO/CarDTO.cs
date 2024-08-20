using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO;

public class CarDTO
{
    public int Id {  get; set; }
    
    public string type { get; set; }
    public string color { get; set; }
    public int doorsNum { get; set; }

    public List<int> drivers { get; set; } = [];
    
    public CarDTO() { }
}
