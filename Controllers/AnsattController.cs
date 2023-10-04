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
    [Route("Ansatte")]
    public class AnsattController : ControllerBase
    {

        [HttpGet("{iKontor}")]
        public IEnumerable<DtoAnsatt> getAnsatteHttp(int iKontor)
        {
            {
                var resultat = getAnsatte(iKontor);

                List<DtoAnsatt> output = new List<DtoAnsatt>();

                foreach (Ansatt ansatt in resultat)
                {
                    string name = ansatt.fornavn + " " + ansatt.etternavn;
                    int ansattNr = ansatt.ansattNr;
                    DtoAnsatt tmp = new DtoAnsatt(name, ansattNr);

                    output.Add(tmp);
                }

                return output;
            }

        }


        public static List<Ansatt> getAnsatte(int iKontor)
        {
            DbQuery soker = new DbQuery();
            return soker.SELECTSql<Ansatt>("SELECT ansattNr, fornavn, etternavn, telefonNr, maxTimerlUka, minTimerlUka " +
                                            "FROM Ansatt, AnsattKontorTilhørighet " +
                                            "WHERE AnsattKontorTilhørighet.Kontor_kontorNr = " + iKontor + " " +
                                            "AND Ansatt.ansattNr = AnsattKontorTilhørighet.Ansatt_ansattNr;");

        }

        [HttpGet("{i}")]
        public List<AnsattMedKontor> getLoginAnsatt(int i) {

            var soker = new DbQuery();

            var sql = "SELECT ansattNr, fornavn, etternavn, telefonNr, Kontor_kontorNr From Ansatt, AnsattKontorTilhørighet WHERE Ansatt.ansattNr = AnsattKontorTilhørighet.Ansatt_ansattNr AND Ansatt.ansattNr = " + i + ";";

            var resultat = soker.SELECTSql<AnsattMedKontor>(sql);

            return resultat;

        }



    }
}

