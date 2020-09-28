using Aramis.TrainMovementData.Data;
using System;
using System.Collections.Generic;

namespace Aramis.TrainMovementData.WebAPI.Data.Interfaces
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> GetVehicles(string trainnumber, DateTime date);
    }
}