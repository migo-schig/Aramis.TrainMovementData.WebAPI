using Aramis.TrainMovementData.Data;
using System.Collections.Generic;

namespace Aramis.TrainMovementData.WebAPI.Data.Interfaces
{
    public interface IGeoDataRepository
    {
        IEnumerable<GeoData> GetAll();
        IEnumerable<GeoData> GetByStation(string station);
    }
}