using Aramis.TrainMovementData.Data;
using System;
using System.Collections.Generic;

namespace Aramis.TrainMovementData.WebAPI.Data.Interfaces
{
    public interface IGeoDataRepository
    {
        IEnumerable<GeoData> GetAll();
        GeoData Get(string station);
        IEnumerable<GeoData> Get(string trainNumber, DateTime date);
        IEnumerable<GeoData> GetLike(string station);
        IEnumerable<GeoData> GetFiltered(int min,
            int max,
            string orderer,
            string operaterString,
            string tractionprovider,
            string traincategory,
            DateTime from,
            DateTime to);
    }
}