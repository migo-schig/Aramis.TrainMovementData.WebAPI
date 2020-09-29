using Aramis.TrainMovementData.Data;
using Aramis.TrainMovementData.WebAPI.Configuration;
using Aramis.TrainMovementData.WebAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Task<List<string>> GetAsync(string stationShort, DateTime dateFrom, DateTime dateTo)
        {
            return context.Notification
                .AsNoTracking()
                .Where(e => e.StationShort == stationShort
                && e.Date >= dateFrom
                && e.Date <= dateTo)
                .Select(e => e.TrainNumber)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<string>> GetLikeAsync(string trainnumber, DateTime dateFrom, DateTime dateTo)
        {
            return context.BasicData
                .AsNoTracking()
                .Where(e =>  e.Date >= dateFrom && e.Date <= dateTo && e.TrainNumber.Contains(trainnumber))
                .Select(e => e.TrainNumber)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
