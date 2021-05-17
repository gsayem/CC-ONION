using CallCentre.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Data.Mappings.Mappings
{
    public class UserMapping : IBaseModelMapper
    {
        public void MapEntity(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            #region Base Class Items
            modelBuilder.Entity<ApplicationUser>().HasKey(s => s.Id).IsClustered(true);
            modelBuilder.Entity<ApplicationUser>().Property(g => g.CreatedDate).IsRequired();
            modelBuilder.Entity<ApplicationUser>().Property(g => g.UpdatedDate).IsRequired();
            #endregion


            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.UserLoginHistories).WithOne(b => b.User).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
