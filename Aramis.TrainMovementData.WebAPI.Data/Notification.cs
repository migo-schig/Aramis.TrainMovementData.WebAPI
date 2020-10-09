﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Aramis.TrainMovementData.Data
{
    public partial class Notification
    {
        public long Id { get; set; }
        public string TrainNumber { get; set; }
        public DateTime Date { get; set; }
        public string NotificationType { get; set; }
        public int StopSequence { get; set; }
        public string StationShort { get; set; }
        public string Station { get; set; }
        public string TrainState { get; set; }
        public DateTime? ScheduledTime { get; set; }
        public DateTime? ActualTime { get; set; }
        public string Source { get; set; }
        public int? DelayMinutes { get; set; }
        public int? AdditionalDelayMinutes { get; set; }
        public DateTime? ProjectedActualTime { get; set; }
        public string ScheduledTrack { get; set; }
        public string ActualTrack { get; set; }
        public string Signal { get; set; }
        public string ScheduledRoute { get; set; }
        public string ActualRoute { get; set; }

        public virtual BasicData BasicData { get; set; }
    }
}