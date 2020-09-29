using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aramis.TrainMovementData.WebAPI.Data.Interfaces
{
    public interface ITrainnumberRepository
    {
        Task<List<string>> GetAsync(string stationShort, DateTime dateFrom, DateTime dateTo);
        Task<List<string>> GetLikeAsync(string trainnumber, DateTime dateFrom, DateTime dateTo);
    }
}