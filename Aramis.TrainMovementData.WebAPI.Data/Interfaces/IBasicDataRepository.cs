using Aramis.TrainMovementData.Data;
using System;
using System.Collections.Generic;

namespace Aramis.TrainMovementData.WebAPI.Data.Interfaces
{
    public interface IBasicDataRepository
    {
        BasicData GetBasicData(string trainnumber, DateTime date);
        IEnumerable<string> GetTrainCategory(string trainCategory);
        IEnumerable<string> GetOrderer(string orderer);
        IEnumerable<string> GetOperator(string operatorString);
        IEnumerable<string> GetTractionProvider(string tractionProvider);
    }
}