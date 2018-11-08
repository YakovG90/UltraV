using System;
using Microsoft.EntityFrameworkCore;

namespace Domain.DataAccess
{
    public class DataContext : IDisposable
    {
        public DataContext(DbContextOptions options, string schema = "ultra")
        {
            this.DbContext = new ApplicationDbContext(options, schema);
        }
        
        public ApplicationDbContext DbContext { get; private set; }

        public void Dispose()
        {
            if (this.DbContext != null)
            {
                this.DbContext.Dispose();
                this.DbContext = null;
            }
            
            GC.SuppressFinalize(this);
        }

        public void MigrateDatabase()
        {
            this.DbContext.Database.Migrate();
        }
    }
}