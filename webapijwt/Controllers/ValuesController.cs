using System.Data.OleDb;
using System.Web.Http;
using webapijwt.Auth;
using webapijwt.Models;

namespace webapijwt.Controllers
{
    public class ValuesController : ApiController
    {
        

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

        [CustomAttribute("Role1","Role3")]
        [Route("api/GetBy")]
        [HttpPost]
        public IHttpActionResult GetBy()
        {
            string authorizationHeader = Request.Headers.Authorization.ToString();
            var authorizationBody = Request.Content;
            string requestBody = authorizationBody.ReadAsStringAsync().Result;
            return Ok("Vô được nghen, " + requestBody);
        }

      
    }
}
