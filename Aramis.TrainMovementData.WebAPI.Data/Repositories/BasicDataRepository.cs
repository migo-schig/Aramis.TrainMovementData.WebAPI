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
    public class BasicDataRepository : IBasicDataRepository
    {
        private readonly AramisDbContext context;
        private readonly AppSettings settings;

        public BasicDataRepository(AramisDbContext context,
            IOptions<AppSettings> config)
        {
            this.context = context;
            this.settings = config.Value;
        }

        public Task<BasicData> GetBasicDataAsync(string trainnumber, DateTime date)
        {
            return context.BasicData
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.TrainNumber == trainnumber
                    && e.Date == date);
        }

        public Task<List<string>> GetTrainCategoryAsync(string trainCategory)
        {
            return context.BasicData
                .AsNoTracking()
                .Select(e => e.TrainCategory)
                .Distinct()
                .Where(e => e.Contains(trainCategory))
                .ToListAsync();
        }
        
        public Task<List<string>> GetOrdererAsync(string orderer)
        {
            return context.BasicData
                .AsNoTracking()
                .Select(e => e.Orderer)
                .Distinct()
                .Where(e => e.Contains(orderer))
                .ToListAsync();
        }
        
        public Task<List<string>> GetOperatorAsync(string operatorString)
        {
            return context.BasicData
                .AsNoTracking()
                .Select(e => e.Operator)
                .Distinct()
                .Where(e => e.Contains(operatorString))
                .ToListAsync();
        }

        public Task<List<string>> GetTractionProviderAsync(string tractionProvider)
        {
            return context.BasicData
                .AsNoTracking()
                .Select(e => e.TractionProvider)
                .Distinct()
                .Where(e => e.Contains(tractionProvider))
                .ToListAsync();
        }
        public BasicData GetBasicDataStation(string station, DateTime from, DateTime to)
        {
            IEnumerable<BasicData> basicdata = context.Notification.Where(e =>
            e.Date <= to
            && e.Date >= from
            && e.Station == station)
                .Join(context.BasicData,
                    n => new { n.Date, n.TrainNumber },
                    b => new { b.Date, b.TrainNumber },
                    (n, b) => b)
                .ToList();
        }
    }
}
