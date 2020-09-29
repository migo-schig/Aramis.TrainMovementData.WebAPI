using Aramis.TrainMovementData.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aramis.TrainMovementData.WebAPI.Data.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetVehiclesAsync(string trainnumber, DateTime date);
    }
}