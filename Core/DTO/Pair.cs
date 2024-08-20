using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    //-------------------------------------------------------------------------------------------------------------------
    //This class Just TO Hold The Name Or Type Of Driver Or Car And Its Id In GetAllLeas Controller
    //-------------------------------------------------------------------------------------------------------------------
    public class Pair
    {
        public int Id {  get; set; }
        public string? Name { get; set; }
    }
}
