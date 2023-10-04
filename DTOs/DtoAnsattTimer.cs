using System;
using ScheduleBackend.Database;
namespace ScheduleBackend.DTOs
{
	public class DtoAnsattTimer
	{
		public int Ansatt_ansattNr { get; set; }
		public string name { get; set; }
		public decimal antallTimerJobbet { get; set; }
		public int minTimerIUka { get; set; }
        public int maxTimerIUka { get; set; }
		public string color { get; set; }
		public int Kontor_kontorNr { get; set; }


        public DtoAnsattTimer(int IAnsatt_ansattNr, string IName, decimal IAntallTimerJobbet, int IMinTimerIUka, int IMaxTimerIUka, int IKontor_kontorNr)
		{
			Ansatt_ansattNr = IAnsatt_ansattNr;
			name = IName;
			antallTimerJobbet = IAntallTimerJobbet;
			minTimerIUka = IMinTimerIUka;
			maxTimerIUka = IMaxTimerIUka;
			color = Farger.getColor(IAnsatt_ansattNr % 10);
			Kontor_kontorNr = IKontor_kontorNr;
		}

		public DtoAnsattTimer()
		{ }
	}
}

