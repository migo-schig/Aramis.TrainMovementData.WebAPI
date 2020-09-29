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
    public class NotificationRepository : INotificationRepository
    {
        private readonly AramisDbContext context;
        private readonly AppSettings settings;

        public NotificationRepository(AramisDbContext context,
            IOptions<AppSettings> config)
        {
            this.context = context;
            this.settings = config.Value;
        }

        public IEnumerable<Notification> GetNotifications(string trainnumber, DateTime date)
        {
            return context.Notification.Where(e => e.TrainNumber == trainnumber
                && e.Date == date)
                .OrderBy(e => e.StopSequence)
                .ToList();
        }
    }
}
