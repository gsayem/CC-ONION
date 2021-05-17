using CallCentre.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Repository
{
    public partial class CallCentreDBContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistories { get; set; }
    }
}
