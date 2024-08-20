using Core.DTO;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.CarDriverServices
{
    public interface ICarDriverServices
    {
        public LeasDTO AddDriverToCar(int carId, int driverId);

        public List<Pair> GetById(int carId);

        public List<LeasDTO> GetAllCarsWithDrivers();


    }
}
