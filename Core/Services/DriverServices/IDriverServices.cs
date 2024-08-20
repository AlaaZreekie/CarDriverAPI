using Core.DTO;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.DriverServices
{
    public interface IDriverServices
    {
        public List<DriverDTO>? GetAllDrivers();


        public DriverDTO? GetById(int id);

        public DriverDTO CreateDriver(string name);
        public DriverDTO? UpdateDriver(int id, string name);

        public DriverDTO? DeleteDriver(int id);
    }
}
