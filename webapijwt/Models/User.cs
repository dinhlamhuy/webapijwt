using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapijwt.Models
{
    public class User
    {
        public string User_ID { get; set; }
        public string FullName { get; set; }
        private string Password { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
    }
    public class LoginRequestModel
    {
        public string User_ID { get; set; }
        public string Password { get; set; }
        public string header { get; set; }

    }
    public class RegisterRequestModel
    {
        public string User_ID { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
    }

    public class ChangePasswordRequestModel
    {
        public string User_ID { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}