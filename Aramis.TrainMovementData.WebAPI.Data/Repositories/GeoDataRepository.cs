using Aramis.TrainMovementData.Data;
using Aramis.TrainMovementData.WebAPI.Configuration;
using Aramis.TrainMovementData.WebAPI.Data.Interfaces;
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

        public IEnumerable<GeoData> GetByStation(string station)
        {
            return context.GeoData.Where(e => e.StationShort == station).ToList();
        }
    }
}
