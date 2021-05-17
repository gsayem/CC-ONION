using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Model
{
    public class ApplicationUser : BaseModel
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string PasswordHash { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<UserLoginHistory> UserLoginHistories { set; get; }

        public string GetFullName()
        {
            return this.GivenName + " " + this.Surname;
        }
    }
}
