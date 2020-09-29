using Aramis.TrainMovementData.Data;
using Aramis.TrainMovementData.WebAPI.Configuration;
using Aramis.TrainMovementData.WebAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<List<GeoData>> GetAllAsync()
        {
            return context.GeoData
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<GeoData> GetAsync(string station)
        {
            return context.GeoData
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.StationShort == station);
        }

        public Task<List<GeoData>> GetLikeAsync(string station)
        {
            return context.GeoData
                .AsNoTracking()
                .Where(e => EF.Functions.Like(e.StationShort, $"%{station}%")
                    || EF.Functions.Like(e.Station, $"%{station}%"))
                .ToListAsync();
        }

        public Task<List<GeoData>> GetAsync(string trainNumber, DateTime date)
        {
            return context.Notification
                .AsNoTracking()
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
                .ToListAsync();
        }

        public Task<List<GeoData>> GetFilteredAsync(int min,
            int max,
            string orderer,
            string operatorString,
            string tractionprovider,
            string traincategory,
            DateTime from,
            DateTime to)
        {
            return context.GeoData
                .AsNoTracking()
                .Join(context.Notification,
                    g => g.StationShort,
                    n => n.StationShort,
                    (g, n) => new { g.Station, g.StationShort, g.Longitude, g.Latitude, n.Date, n.TrainNumber })
                .Join(context.BasicData,
                    gn => new { gn.TrainNumber, gn.Date },
                    b => new { b.TrainNumber, b.Date },
                    (gn, b) => new { gn.Station, gn.StationShort, gn.Longitude, gn.Latitude, gn.Date, gn.TrainNumber, b.TrainCategory, b.Operator, b.Orderer, b.TractionProvider })
                .Where(joined => (joined.Orderer.Contains(orderer) || string.IsNullOrEmpty(orderer))
                    && (joined.Operator.Contains(operatorString) || string.IsNullOrEmpty(operatorString))
                    && (joined.TractionProvider.Contains(tractionprovider) || string.IsNullOrEmpty(tractionprovider))
                    && (joined.TrainCategory.Contains(traincategory) || string.IsNullOrEmpty(traincategory))
                    && joined.Date >= from && joined.Date <= to)
                .GroupBy(joined => new { joined.StationShort, joined.Station, joined.Latitude, joined.Longitude })
                .Select(joined => new { joined.Key.Latitude, joined.Key.Longitude, joined.Key.Station, joined.Key.StationShort, Count = joined.Count() })
                .Where(grouped => grouped.Count >= min && grouped.Count <= max)
                .Select(grouped => new GeoData { Latitude = grouped.Latitude, Longitude = grouped.Longitude, Station = grouped.Station, StationShort = grouped.StationShort })
                .ToListAsync();
        }
    }
}
