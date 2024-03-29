﻿using System;

namespace Common
{
    public static class Utils
    {
        public static DateTime GetCurrentDate()
        {
            DateTime date = DateTime.Now;
            return date.Date;
        }

        public static string GetCurrentEnvironment() 
        {
             string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return environment;
        }
    }
}
