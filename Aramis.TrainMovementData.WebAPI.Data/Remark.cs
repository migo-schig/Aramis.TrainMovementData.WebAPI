﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Aramis.TrainMovementData.Data
{
    public partial class Remark
    {
        public long Id { get; set; }
        public string TrainNumber { get; set; }
        public DateTime Date { get; set; }
        public string RemarkType { get; set; }
        public string StationShortFrom { get; set; }
        public string StationFrom { get; set; }
        public string StationShortTo { get; set; }
        public string StationTo { get; set; }
        public TimeSpan? ScheduledTime { get; set; }
        public TimeSpan? InputTime { get; set; }
        public string Notes { get; set; }

        public virtual BasicData BasicData { get; set; }
    }
}