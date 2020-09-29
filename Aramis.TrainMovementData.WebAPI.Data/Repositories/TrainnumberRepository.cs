using Aramis.TrainMovementData.Data;
using Aramis.TrainMovementData.WebAPI.Configuration;
using Aramis.TrainMovementData.WebAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aramis.TrainMovementData.WebAPI.Data.Repositories
{
    public class TrainnumberRepository : ITrainnumberRepository
    {

        private readonly AramisDbContext context;
        private readonly AppSettings settings;

        public TrainnumberRepository(AramisDbContext context,
            IOptions<AppSettings> config)
        {
            this.context = context;
            settings = config.Value;
        }

        public IEnumerable<string> Get(string stationShort, DateTime dateFrom, DateTime dateTo)
        {
            return context.Notification
                .Where(e => e.StationShort == stationShort
                && e.Date >= dateFrom
                && e.Date <= dateTo)
                .Select(e => e.TrainNumber)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<string> GetLike(string trainnumber, DateTime dateFrom, DateTime dateTo)
        {
            return context.BasicData
                .Where(e =>  e.Date >= dateFrom && e.Date <= dateTo && e.TrainNumber.Contains(trainnumber))
                .Select(e => e.TrainNumber)
                .AsNoTracking()
                .ToList();
        }
    }
}
