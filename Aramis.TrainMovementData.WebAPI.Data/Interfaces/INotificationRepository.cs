using Aramis.TrainMovementData.Data;
using System;
using System.Collections.Generic;

namespace Aramis.TrainMovementData.WebAPI.Data.Interfaces
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNotifications(string trainnumber, DateTime date);
    }
}