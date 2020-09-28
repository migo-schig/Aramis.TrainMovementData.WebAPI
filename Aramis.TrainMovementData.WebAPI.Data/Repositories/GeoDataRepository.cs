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
                        notification.StopSequence
                    })
                .OrderBy(e => e.StopSequence)
                .Select(e => e.GeoData)
                .ToList();

            return stations;
        }

        public IEnumerable<GeoData> GetFiltered(int min,
            int max,
            string orderer,
            string operaterString,
            string tractionprovider,
            string traincategory,
            DateTime from,
            DateTime to)
        {
            return context.GeoData
                .Join(context.Notification,
                    g => g.StationShort,
                    n => n.StationShort,
                    (g, n) => new { g.Station, g.StationShort, g.Longitude, g.Latitude, n.Date, n.TrainNumber })
                .Join(context.BasicData,
                    gn => new { gn.TrainNumber, gn.Date },
                    b => new { b.TrainNumber, b.Date },
                    (gn, b) => new { gn.Station, gn.StationShort, gn.Longitude, gn.Latitude, gn.Date, gn.TrainNumber, b.TrainCategory, b.Operator, b.Orderer, b.TractionProvider })
                .Where(joined => joined.Orderer.Contains(orderer)
                    && joined.Operator.Contains(operaterString)
                    && joined.TractionProvider.Contains(tractionprovider)
                    && joined.TrainCategory.Contains(traincategory)
                    && joined.Date >= from && joined.Date <= to)
                .GroupBy(joined => new { joined.StationShort, joined.Station, joined.Latitude, joined.Longitude })
                .Select(joined => new { joined.Key.Latitude, joined.Key.Longitude, joined.Key.Station, joined.Key.StationShort, Count = joined.Count() })
                .Where(grouped => grouped.Count >= min && grouped.Count <= max)
                .Select(grouped => new GeoData { Latitude = grouped.Latitude, Longitude = grouped.Longitude, Station = grouped.Station, StationShort = grouped.StationShort })
                .ToList();
        }
    }
}
