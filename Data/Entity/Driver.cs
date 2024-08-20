using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Driver : TEntity
    {
        public string? Name { get; set; }

        public ICollection<Leas> Leas { get; set; } = [];        
    }
}
