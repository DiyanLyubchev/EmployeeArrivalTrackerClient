﻿using System;

namespace Common.Models.HealthCheck
{
    public class HealthInfo
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
    }
}
