using System;
using System.Collections.Generic;

namespace Aramis.TrainMovementData.WebAPI.Data.Interfaces
{
    public interface ITrainnumberRepository
    {
        IEnumerable<string> Get(string stationShort, DateTime dateFrom, DateTime dateTo); 
    }
}