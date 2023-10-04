using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ScheduleBackend.Entity;
using Microsoft.Data.SqlClient;
using ScheduleBackend.Database;
using ScheduleBackend.DTOs;

namespace ScheduleBackend.Controllers
{
    [ApiController]
    [Route("statistics")]
    public class StatisticsController : ControllerBase
    {

        [HttpGet("alle/{Iuke}/{Ikontor}")]
        public decimal getTotaltJobbetIUke(int IUke, int Ikontor)
        {
            var resultat = getAntallTimerPerAnsatt(IUke, Ikontor);

            decimal output = 0;

            foreach (DtoAnsattTimer skift in resultat)
            {
                output = output + skift.antallTimerJobbet;
            }

            return output;
        }


        [HttpGet("{Iuke}/{Ikontor}")]
        public IEnumerable<DtoAnsattTimer> getAntallTimerPerAnsatt(int IUke, int Ikontor)
        {
            DbQuery soker = new DbQuery();

            //Resultat inneholder alle skift i uke x
            var resultat = soker.SELECTSql<SkiftMedTimer>("SELECT skiftStart, Ansatt_ansattNr, skiftSlutt, fornavn, etternavn, minTimerlUka, maxTimerlUka " +
                                                  "FROM Skift, Ansatt " +
                                                  "WHERE DATEPART(ww, skiftStart) = " + IUke + " " +
                                                  "AND Skift.Ansatt_ansattNr = Ansatt.ansattNr " +
                                                  "AND Skift.Kontor_kontorNr = " + Ikontor + ";");

            List<DtoAnsattTimer> omgjortListe = new List<DtoAnsattTimer>();
            List<DtoAnsattTimer> output = new List<DtoAnsattTimer>();

            //Denne loopen gjør om alle skiftene til å ikke inneholde start og slutt, men kun antall timer jobbet
            foreach (SkiftMedTimer skift in resultat)
            {
                int ansattNr = skift.Ansatt_ansattNr;
                string name = skift.fornavn + " " + skift.etternavn;
                decimal antallTimerJobbet = kalkulerTimer(skift.skiftStart, skift.skiftSlutt); //Kalkuler timer metoden er definer lenger nede i samme fil
                int minTimerIUka = skift.minTimerIUka;
                int maxTimerIUka = skift.maxTimerIUka;

                DtoAnsattTimer tmp = new DtoAnsattTimer(ansattNr, name, antallTimerJobbet, minTimerIUka, maxTimerIUka, Ikontor);

                omgjortListe.Add(tmp);
            }




                //Denne loopen summerer timene til skiftene basert på hvem som har jobbet dem
                string navn = "";
                decimal antallTimer = 0;
                int minTimerIUka2 = 0;
                int maxTimerIUka2 = 0;
                int ansattNrIndex;

                if (omgjortListe.Count() != 0)
                {
                    ansattNrIndex = omgjortListe.First().Ansatt_ansattNr;
                }

                else
                {
                    ansattNrIndex = 0;
                }



                foreach (DtoAnsattTimer skift in omgjortListe)
                {
                    if (ansattNrIndex == skift.Ansatt_ansattNr)
                    {
                        navn = skift.name;
                        antallTimer = antallTimer + skift.antallTimerJobbet;
                        minTimerIUka2 = skift.minTimerIUka;
                        maxTimerIUka2 = skift.maxTimerIUka;
                    }

                    else
                    {
                        DtoAnsattTimer tmp = new DtoAnsattTimer(ansattNrIndex, navn, antallTimer, minTimerIUka2, maxTimerIUka2, Ikontor);
                        output.Add(tmp);

                        navn = skift.name;
                        antallTimer = 0 + skift.antallTimerJobbet;
                        ansattNrIndex = skift.Ansatt_ansattNr;
                        minTimerIUka2 = skift.minTimerIUka;
                        maxTimerIUka2 = skift.maxTimerIUka;
                    }
                }
                DtoAnsattTimer tmpSiste = new DtoAnsattTimer(ansattNrIndex, navn, antallTimer, minTimerIUka2, maxTimerIUka2, Ikontor);
                output.Add(tmpSiste);

            var alleAnsatte = AnsattController.getAnsatte(Ikontor);


            List<DtoAnsattTimer> sisteOutput = new List<DtoAnsattTimer>();
            Boolean funnet = false;
            foreach (Ansatt ansatt in alleAnsatte)
            {
                funnet = false;
                foreach (DtoAnsattTimer ansatt2 in output)
                {
                    if (ansatt.ansattNr == ansatt2.Ansatt_ansattNr)
                    {
                        DtoAnsattTimer tmp = new DtoAnsattTimer(ansatt2.Ansatt_ansattNr, ansatt2.name, ansatt2.antallTimerJobbet, ansatt2.minTimerIUka, ansatt2.maxTimerIUka, Ikontor);
                        sisteOutput.Add(tmp);

                        funnet = true;

                        
                    }
                }
                if (!funnet)
                {
                    string navn2 = ansatt.fornavn + " " + ansatt.etternavn;
                    DtoAnsattTimer tmp2 = new DtoAnsattTimer(ansatt.ansattNr, navn2, 0, ansatt.minTimerIUka, ansatt.maxTimerIUka, Ikontor);
                    sisteOutput.Add(tmp2);
                }
            }

            return sisteOutput;
        }






        public static decimal kalkulerTimer(DateTime start, DateTime slutt)
        {
            var ts = slutt - start;

            double minutter = ts.TotalMinutes;

            return (decimal)(minutter / 60);
        }



    }
}

