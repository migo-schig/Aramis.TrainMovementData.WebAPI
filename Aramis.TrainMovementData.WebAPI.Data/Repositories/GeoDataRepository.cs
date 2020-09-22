using Aramis.TrainMovementData.Data;
using Aramis.TrainMovementData.WebAPI.Configuration;
using Aramis.TrainMovementData.WebAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aramis.TrainMovementData.WebAPI.Data.Repositories
{
    public class GeoDataRepository : IGeoDataRepository
    {
        private readonly AramisDbContext context;
        private readonly AppSettings settings;

        public GeoDataRepository(AramisDbContext context,
            IOptions<AppSettings> config)
        {
            this.context = context;
            this.settings = config.Value;
        }

        public IEnumerable<GeoData> GetAll()
        {
            return context.GeoData.ToList();
        }

        public GeoData Get(string station)
        {
            return context.GeoData.FirstOrDefault(e => e.StationShort == station);
        }

        public IEnumerable<GeoData> GetLike(string station)
        {
            return context.GeoData.Where(e => EF.Functions.Like(e.StationShort, $"%{station}%")
            || EF.Functions.Like(e.Station, $"%{station}%"))
            .ToList();
        }

        public IEnumerable<GeoData> Get(string trainNumber, DateTime date)
        {
            IEnumerable<GeoData> stations = context.Notification
                .Distinct()
                .Where(e => e.TrainNumber == trainNumber && e.Date == date)
                .Join(
                    context.GeoData,
                    notification => notification.StationShort,
                    geoData => geoData.StationShort,
                    (notification, geoData) => new
                    {
                        GeoData = geoData,
                        StopSequence = notification.StopSequence
                    })
                .OrderBy(e => e.StopSequence)
                .Select(e => e.GeoData)
                .ToList();

            return stations;
        }
    }
}
