using System;
using Microsoft.Data.SqlClient;

namespace ScheduleBackend.DTOs
{
    public class DtoLogin
    {
        public string brukernavn { get; set; }
        public string passord { get; set; }

        public DtoLogin()
        {
        }


        public string GetBrukernavn()
        {
            return this.brukernavn;
        }

        public string GetPassord()
        {
            return this.passord;
        }
    }
}

