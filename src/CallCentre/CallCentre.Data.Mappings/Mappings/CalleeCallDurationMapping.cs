using CallCentre.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Data.Mappings.Mappings
{
    public class CalleeCallDurationMapping : IBaseModelMapper
    {
        public void MapEntity(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CalleeCallDuration>().ToTable("CalleeCallDurations");
            #region Base Class Items
            modelBuilder.Entity<CalleeCallDuration>().HasKey(s => s.Id).IsClustered(true);
            modelBuilder.Entity<CalleeCallDuration>().Property(g => g.CreatedDate).IsRequired();
            modelBuilder.Entity<CalleeCallDuration>().Property(g => g.UpdatedDate).IsRequired();
            #endregion

            modelBuilder.Entity<CalleeCallDuration>().Property(g => g.CallDateTime);
            modelBuilder.Entity<CalleeCallDuration>().Property(g => g.CallDurationInSecond);
            modelBuilder.Entity<CalleeCallDuration>().Property(g => g.Charge).HasPrecision(18, 4);

        }
    }
}
