using CallCentre.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace CallCentre.Data.Mappings.Mappings
{
    public class CostingTimeSlotMapping : IBaseModelMapper
    {
        public void MapEntity(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CostingTimeSlot>().ToTable("CostingTimeSlots");
            #region Base Class Items
            modelBuilder.Entity<CostingTimeSlot>().HasKey(s => s.Id).IsClustered(true);
            modelBuilder.Entity<CostingTimeSlot>().Property(g => g.CreatedDate).IsRequired();
            modelBuilder.Entity<CostingTimeSlot>().Property(g => g.UpdatedDate).IsRequired();
            #endregion

            modelBuilder.Entity<CostingTimeSlot>().Property(g => g.StartHour).IsRequired();
            modelBuilder.Entity<CostingTimeSlot>().Property(g => g.StartMinute).IsRequired();
            modelBuilder.Entity<CostingTimeSlot>().Property(g => g.EndHour).IsRequired();
            modelBuilder.Entity<CostingTimeSlot>().Property(g => g.EndMinute).IsRequired();
            modelBuilder.Entity<CostingTimeSlot>().Property(g => g.CostPerMinute).IsRequired();

        }
    }
}
