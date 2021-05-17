using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCentreWeb.Models
{
    public class LoginViewModel
    {
        public string Password { get; set; }
        public string EmailAddress { get; set; }
    }
    public class UserViewModel
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class UserLoginHistoryViewModel
    {
        public string UserId { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string LastLogin { get; set; }
    }
}
