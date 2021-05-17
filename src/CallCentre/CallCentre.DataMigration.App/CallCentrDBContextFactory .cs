using CallCentre.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using CallCentre.Data.Mappings;

namespace CallCentre.DataMigration.App
{
    public class CallCentrDBContextFactory : IDesignTimeDbContextFactory<CallCentreDBContext>
    {
        public CallCentreDBContext CreateDbContext(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<CallCentreDBContext>().UseSqlServer(connectionString, b => b.MigrationsAssembly("CallCentre.DataMigration.App")).UseLazyLoadingProxies();
            builder.UseCalCentreModel(connectionString);
            return new CallCentreDBContext(builder.Options);
        }
        /// <summary>
        ///Called by dotnet ef database update.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public CallCentreDBContext CreateDbContext(string[] args)
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("CC_API_SQL_CONNECTION"));
            return CreateDbContext(Environment.GetEnvironmentVariable("CC_API_SQL_CONNECTION"));
        }
    }
}
