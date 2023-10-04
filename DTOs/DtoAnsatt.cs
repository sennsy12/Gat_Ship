using System;
using Microsoft.Data.SqlClient;

namespace ScheduleBackend.DTOs
{
	public class DtoAnsatt
	{

		public string name { get; set; }
		public int ansatt_ansattNr { get; set; }


		public DtoAnsatt()
		{
		}

		public DtoAnsatt(string IName, int IAnsatt_ansattNr)
		{
			name = IName;
			ansatt_ansattNr = IAnsatt_ansattNr;
		}

	}
}

