using System;
namespace ScheduleBackend.DTOs
{
	public class DtoLoginKontor
	{

		public string brukernavn { get; set; }
		public int Kontor_kontorNr { get; set; }
		public string navn { get; set; }
		public string epost { get; set; }
		public int telefonNr { get; set; }

		public DtoLoginKontor()
		{
		}
	}
}

