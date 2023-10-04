using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ScheduleBackend.Entity;
using Microsoft.Data.SqlClient;
using ScheduleBackend.Database;
using ScheduleBackend.DTOs;
using System.Globalization;


namespace ScheduleBackend.Controllers
{
    [ApiController]
    [Route("Skift")]
    public class SkiftKontroller : ControllerBase
    {

        public static DateTime GetFirstDayOfWeek(int weekNumber, int year)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekNumber;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        [HttpGet("{IKontor}/{IUke}")]
        public IEnumerable<DtoEnkelSkift> getSkiftSkjekk(int IKontor, int IUke)
        {
            var soker = new DbQuery();
            var resultat = soker.SELECTSql<Skift>("SELECT" +
                " skiftStart, Ansatt.ansattNr, skiftSlutt, Ansatt.fornavn, Ansatt.etternavn " +
                "FROM Skift, Ansatt " +
                "WHERE Ansatt.ansattNr = Skift.Ansatt_ansattNr " +
                "AND DATEPART(ww, skiftStart) = " + IUke +" " +
                "AND Skift.Kontor_kontorNr = " + IKontor + ";");

            List<Skift> sortert = resultat.OrderBy(o => o.skiftStart).ToList();

            DateTime tmpStart = GetFirstDayOfWeek(IUke, 2023);
            DateTime tmpEnd = GetFirstDayOfWeek(IUke, 2023);
            int counter = 0;

            List<DtoEnkelSkift> output = new List<DtoEnkelSkift>();

            foreach (Skift skift in sortert)
            {
                //Kun første gangen loopen kjøres
                if(counter == 0)
                {
                    if(skift.skiftStart > tmpStart)
                    tmpEnd = skift.skiftStart;
                    DtoEnkelSkift tmp = new DtoEnkelSkift(tmpStart, tmpEnd);
                    output.Add(tmp);

                    tmpStart = skift.skiftStart;
                    tmpEnd = skift.skiftSlutt;
                    counter++;
                }

                if(skift.skiftStart > tmpEnd)
                {
                    DtoEnkelSkift tmp = new DtoEnkelSkift(tmpEnd, skift.skiftStart);
                    output.Add(tmp);

                    tmpStart = skift.skiftStart;
                    tmpEnd = skift.skiftSlutt;
                }
                else
                {
                    tmpEnd = skift.skiftSlutt;
                }
            }

            return output;


        }




        [HttpGet("{input}")]
        public IEnumerable<DtoSkift> GetSkift(int input)
        {
            var soker = new DbQuery();

            var resultat = soker.SELECTSql<Skift>("SELECT" +
                " skiftStart, Ansatt.ansattNr, skiftSlutt, Ansatt.fornavn, Ansatt.etternavn " +
                "FROM Skift, Ansatt WHERE Ansatt.ansattNr = Skift.Ansatt_ansattNr " +
                "AND Skift.Kontor_kontorNr = " + input + ";");

            List<DtoSkift> output = new List<DtoSkift>();

            foreach (Skift skift in resultat)
            {
                string name = skift.fornavn + " " + skift.etternavn;
                int Ansatt_ansattNr = skift.Ansatt_ansattNr;
                DateTime start = skift.skiftStart;
                DateTime end = skift.skiftSlutt;
                string color = "blue";

                DtoSkift tmp = new DtoSkift(name, Ansatt_ansattNr, start, end, color);

                output.Add(tmp);
            }

            return output;
        }

        [HttpGet("ansatt/{Ians}")]
        public IEnumerable<DtoSkift> getSkiftAnsatt(int Ians)
        {
            var soker = new DbQuery();

            var resultat = soker.SELECTSql<Skift>("SELECT" +
                " skiftStart, Ansatt.ansattNr, skiftSlutt, Ansatt.fornavn, Ansatt.etternavn " +
                "FROM Skift, Ansatt WHERE Ansatt.ansattNr = Skift.Ansatt_ansattNr " +
                "AND Skift.Ansatt_ansattNr = " + Ians + " ;");

            List<DtoSkift> output = new List<DtoSkift>();

            foreach (Skift skift in resultat)
            {
                string name = skift.fornavn + " " + skift.etternavn;
                int Ansatt_ansattNr = skift.Ansatt_ansattNr;
                DateTime start = skift.skiftStart;
                DateTime end = skift.skiftSlutt;
                string color = "blue";

                DtoSkift tmp = new DtoSkift(name, Ansatt_ansattNr, start, end, color);

                output.Add(tmp);
            }

            return output;
        }

        [HttpPost]
        public Boolean SetSkift([FromBody] DtoCreateSkift input)
        {
            var soker = new DbQuery();

            String sql = "INSERT INTO Skift (skiftStart, Ansatt_ansattNr, skiftSlutt, Kontor_kontorNr)" +
                         "VALUES (@ISkiftStart, @IAnsatt_ansattNr, @ISkiftSlutt, @IKontor_kontorNr);";

            var parameters = new List<SqlParameter>();
            // Legg til @id parameter med verdien som sendes inn (id).
            parameters.Add(new SqlParameter("ISkiftStart", DateTime.Parse(input.start)));
            parameters.Add(new SqlParameter("IAnsatt_ansattNr", input.Ansatt_ansattNr));
            parameters.Add(new SqlParameter("ISkiftSlutt", DateTime.Parse(input.end)));
            parameters.Add(new SqlParameter("IKontor_kontorNr", input.Kontor_kontorNr));

            var result = soker.INSERTSql(sql, parameters);

            if (result == 1)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        [HttpPost("slett")]
        public Boolean slettSkift([FromBody] DtoSkift input)
        {
            var soker = new DbQuery();

            String sql = "DELETE FROM Skift WHERE skiftStart = @ISkiftStart " +
                         "AND Ansatt_ansattNr = @IAnsatt_ansattNr;";

            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("ISkiftStart", input.start));
            parameters.Add(new SqlParameter("IAnsatt_ansattNr", input.ansatt_ansattNr));

            var result = soker.INSERTSql(sql, parameters);

            if (result == 1)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

    }
}

