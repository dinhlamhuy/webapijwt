using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using webapijwt.Auth;
using webapijwt.Models;

namespace webapijwt.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {

            string token= JwtManager.GenerateToken("54314");
            return new string[] { "value1", "value2", token };
        }

        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult Login(LoginRequestModel req)
        {
            string userName = req.User_ID;
            string password = req.Password;

            string result = JwtManager.GenerateToken(userName+password);
          
                var response = new
                {
                    Message = 1,
                    USR = userName,
                    PAS = password,
                    Data = result
                };
                return Ok(response);
            
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
