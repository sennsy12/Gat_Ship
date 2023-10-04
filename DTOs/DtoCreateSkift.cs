using System;
using Microsoft.Data.SqlClient;

namespace ScheduleBackend.DTOs
{
    public class DtoCreateSkift
    {

        public int Ansatt_ansattNr { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public int Kontor_kontorNr { get; set; }

        public DtoCreateSkift()
        {
        }



    }
}

