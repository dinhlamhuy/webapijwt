using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using webapijwt.Auth;
using webapijwt.Models;

namespace webapijwt.Controllers
{
    public class ValuesController : ApiController
    {
        //[Route("api/value")]
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    string token = JwtManager.GenerateToken("54314");
        //    return new string[] { "value1", "value2", token };
        //}

        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult Login(LoginRequestModel req)
        {
            string userName = req.User_ID;
            string password = req.Password;
     
            string sSQL = @"SELECT TOP 1 User_Serial_Key, User_Login, Password_Login, User_Full_Name, User_Level 
                            FROM USER_AD WHERE User_Login = '" + userName + @"' 
                            AND Password_Login = '" + password + "' ";
            OleDbConnection odcConnect = new OleDbConnection(sqlConnect.Connect_String);
            OleDbCommand odcCommand = new OleDbCommand(sSQL, odcConnect);
            odcCommand.CommandTimeout = 120;
            odcConnect.Open();
            OleDbDataReader odr = odcCommand.ExecuteReader();
            if (odr.HasRows)
            {
                while (odr.Read())
                {
                    string result = JwtManager.GenerateToken("Admin", "Role1");

                    var responsedata = new
                    {
                        Message = 1,
                        USR = odr["User_Login"],
                        Key = odr["User_Serial_Key"],
                        Token = result
                    };
                    return Ok(responsedata);
                }
            }
            var response = new
            {
                Message = "Mật khẩu không đúng"
            };
            return Ok(response);


        }

        [CustomAttribute("Role1,Role3")]
        [Route("api/GetBy")]
        [HttpPost]
        public IHttpActionResult GetBy()
        {
            string authorizationHeader = Request.Headers.Authorization.ToString();
            var authorizationBody = Request.Content;
            string requestBody = authorizationBody.ReadAsStringAsync().Result;
            return Ok("Welcome, " + requestBody);
        }

        //// POST api/values
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
