using CallCentre.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Repository
{
    public partial class CallCentreDBContext
    {
        public DbSet<Callee> Callees { get; set; }
        public DbSet<CalleeCallDuration> CalleeCallDurations { get; set; }
        public DbSet<CostingTimeSlot> CostingTimeSlots { get; set; }
    }
}
