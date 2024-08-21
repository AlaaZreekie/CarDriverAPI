using Core.DTO;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.CarDriverServices
{
    public interface ILeaseServices
    {
        public LeasDTO? AddDriverToCar(int carId, int driverId);

        public List<Pair>? GetById(int carId);

        public List<LeasDTO> GetAllLease();

        public LeasDTO? CreateLease(int carId, int driverId, DateTime StartDate, DateTime EndDate);


    }
}
