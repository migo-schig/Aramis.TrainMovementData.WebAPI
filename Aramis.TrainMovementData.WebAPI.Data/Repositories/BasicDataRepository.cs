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

        public BasicData GetBasicData(string trainnumber, DateTime date)
        {
            return context.BasicData.FirstOrDefault(e => e.TrainNumber == trainnumber
                && e.Date == date);
        }

        public IEnumerable<string> GetTrainCategory(string trainCategory)
        {
            return context.BasicData
                .Select(e => e.TrainCategory)
                .Distinct()
                .Where(e => e.Contains(trainCategory))
                .ToList();
        }
        
        public IEnumerable<string> GetOrderer(string orderer)
        {
            return context.BasicData
                .Select(e => e.Orderer)
                .Distinct()
                .Where(e => e.Contains(orderer))
                .ToList();
        }
        
        public IEnumerable<string> GetOperator(string operatorString)
        {
            return context.BasicData
                .Select(e => e.Operator)
                .Distinct()
                .Where(e => e.Contains(operatorString))
                .ToList();
        }

        public IEnumerable<string> GetTractionProvider(string tractionProvider)
        {
            return context.BasicData
                .Select(e => e.TractionProvider)
                .Distinct()
                .Where(e => e.Contains(tractionProvider))
                .ToList();
        }
    }
}
