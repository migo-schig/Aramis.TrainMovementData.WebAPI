using Aramis.TrainMovementData.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aramis.TrainMovementData.WebAPI.Data.Interfaces
{
    public interface IGeoDataRepository
    {
        Task<List<GeoData>> GetAllAsync();
        Task<GeoData> GetAsync(string station);
        Task<List<GeoData>> GetAsync(string trainNumber, DateTime date);
        Task<List<GeoData>> GetLikeAsync(string station);
        Task<List<GeoData>> GetFilteredAsync(int min,
            int max,
            string orderer,
            string operaterString,
            string tractionprovider,
            string traincategory,
            DateTime from,
            DateTime to);
    }
}