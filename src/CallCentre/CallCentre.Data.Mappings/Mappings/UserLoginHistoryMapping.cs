using CallCentre.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Data.Mappings.Mappings
{
    public class UserLoginHistoryMapping : IBaseModelMapper
    {
        public void MapEntity(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserLoginHistory>().ToTable("UserLoginHistories");
            #region Base Class Items
            modelBuilder.Entity<UserLoginHistory>().HasKey(s => s.Id).IsClustered(true);
            modelBuilder.Entity<UserLoginHistory>().Property(g => g.CreatedDate).IsRequired();
            modelBuilder.Entity<UserLoginHistory>().Property(g => g.UpdatedDate).IsRequired();
            #endregion


            
        }
    }
}
