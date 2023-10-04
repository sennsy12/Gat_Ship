using System;
using Microsoft.Data.SqlClient;
using ScheduleBackend.Database;

namespace ScheduleBackend.DTOs
{
	public class DtoSkift
	{

		public string name { get; set; }
		public int ansatt_ansattNr { get; set; }
		public DateTime start { get; set; }
		public DateTime end { get; set; }
		public string color { get; set; }

		public DtoSkift()
		{
		}

		public DtoSkift(string INAme, int IAnsatt_ansattNr, DateTime IStart, DateTime IEnd, string IColor)
		{
			name = INAme;
			ansatt_ansattNr = IAnsatt_ansattNr;
			start = IStart;
			end = IEnd;
			color = Farger.getColor(IAnsatt_ansattNr % 10);
		}



	}
}

