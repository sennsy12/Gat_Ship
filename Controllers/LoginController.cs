using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ScheduleBackend.Entity;
using Microsoft.Data.SqlClient;
using ScheduleBackend.Database;
using ScheduleBackend.DTOs;
using Microsoft.Extensions.Options; // endring fra her
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace ScheduleBackend.Controllers
{
    [ApiController]
    [Route("login")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public LoginController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        private string GenerateJwtToken(DtoLogin loginModel)
        {

            var soker = new DbQuery();
            String sql = "SELECT brukernavn, Kontor_kontorNr FROM Inlogging WHERE brukernavn = '" + loginModel.brukernavn + "' AND passord = '" + loginModel.passord + "';";
            var login = soker.SELECTSql<LoginCount>(sql).FirstOrDefault();
            string sqlAccount = "SELECT * From Kontor WHERE kontorNr = " + login.Kontor_kontorNr  + ";";
            var resultat = soker.SELECTSql<Kontor>(sqlAccount).FirstOrDefault();

           


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("AAAAAAAAAAAAAAAAAAAAA");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, loginModel.brukernavn),
                new Claim("kontorNr", resultat.kontorNr.ToString()),
                new Claim("navn", resultat.navn.ToString()),
                new Claim("epost", resultat.epost.ToString()),
                new Claim("telefonNr", resultat.telefonNr.ToString())
                
                // Add other claims as needed
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] DtoLogin loginModel)
        {
            if (this.IsValidUser(loginModel))
            {
                var token = GenerateJwtToken(loginModel);

                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        public bool IsValidUser(DtoLogin loginModel)
        {
            // Implement your user validation logic here
            return true;
        }




        [HttpPost]
        public IActionResult SkjekkLogin([FromBody] DtoLogin input)
        {
            string brukernavn = input.GetBrukernavn();
            string passord = input.GetPassord();
            var soker = new DbQuery();

            String sql = "SELECT brukernavn, Kontor_kontorNr FROM Inlogging WHERE brukernavn = '" + brukernavn + "' AND passord = '" + passord + "';";

            var resultat = soker.SELECTSql<LoginCount>(sql);

            

           

            foreach (LoginCount login in resultat)
            {
                if (resultat.Count == 1)
                {
                    if (this.IsValidUser(input))
                    {
                        var token = GenerateJwtToken(input);

                        return Ok(new { Token = token + login.Kontor_kontorNr });
                    }
                }
            }

            return Unauthorized();

        }

        public static IEnumerable<Kontor> getKontor(int input)
        {
            var soker = new DbQuery();
            string sql = "SELECT * From Kontor WHERE kontorNr = " + input + ";";

            var resultat = soker.SELECTSql<Kontor>(sql);

            return resultat;

        }

        [HttpGet("test/{input}")]
        public IEnumerable<Kontor> getKontorTest(int input)
        {
            return getKontor(input);
        }

    }
}
    


