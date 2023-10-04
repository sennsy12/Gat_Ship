using System;
using Microsoft.Data.SqlClient;
using ScheduleBackend.Database;

namespace ScheduleBackend.DTOs
{
    public class DtoEnkelSkift
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int startDay { get; set; }
        public int endDay { get; set; }

        public DtoEnkelSkift()
        {
        }

        public DtoEnkelSkift(DateTime IStart, DateTime IEnd)
        {
            start = IStart;
            end = IEnd;
            startDay = (int)IStart.DayOfWeek;
            endDay = (int)IEnd.DayOfWeek;

        }



    }
}

