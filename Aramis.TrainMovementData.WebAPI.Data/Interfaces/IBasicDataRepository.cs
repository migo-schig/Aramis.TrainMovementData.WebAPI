using Aramis.TrainMovementData.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aramis.TrainMovementData.WebAPI.Data.Interfaces
{
    public interface IBasicDataRepository
    {
        Task<BasicData> GetBasicDataAsync(string trainnumber, DateTime date);
        Task<List<string>> GetTrainCategoryAsync(string trainCategory);
        Task<List<string>> GetOrdererAsync(string orderer);
        Task<List<string>> GetOperatorAsync(string operatorString);
        Task<List<string>> GetTractionProviderAsync(string tractionProvider);
    }
}