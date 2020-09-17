using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aramis.TrainMovementData.WebAPI.Configuration
{
    public class AppSettings
    {
        public string SourceFileFolder { get; set; }
        public string ProcessedFileFolder { get; set; }
    }
}
