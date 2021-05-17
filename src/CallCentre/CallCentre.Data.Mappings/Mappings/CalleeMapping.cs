using CallCentre.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Data.Mappings.Mappings
{
    public class CalleeMapping : IBaseModelMapper
    {
        public void MapEntity(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Callee>().ToTable("Callees");
            #region Base Class Items
            modelBuilder.Entity<Callee>().HasKey(s => s.Id).IsClustered(true);
            modelBuilder.Entity<Callee>().Property(g => g.CreatedDate).IsRequired();
            modelBuilder.Entity<Callee>().Property(g => g.UpdatedDate).IsRequired();
            #endregion

            modelBuilder.Entity<Callee>().Property(g => g.FirstName);
            modelBuilder.Entity<Callee>().Property(g => g.FirstName);
            modelBuilder.Entity<Callee>().Property(g => g.Phone);
            modelBuilder.Entity<Callee>().Property(g => g.Address);

            modelBuilder.Entity<Callee>().HasMany(c => c.CalleeCallDurations).WithOne(b => b.Callee).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
