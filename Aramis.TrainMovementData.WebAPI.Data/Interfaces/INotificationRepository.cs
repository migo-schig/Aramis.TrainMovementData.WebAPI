using Aramis.TrainMovementData.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aramis.TrainMovementData.WebAPI.Data.Interfaces
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetNotificationsAsync(string trainnumber, DateTime date);
    }
}