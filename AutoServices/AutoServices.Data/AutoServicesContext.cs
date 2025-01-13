using AutoServices.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AutoServices.Data
{
    public class AutoServicesContext : DbContext
    {
        public AutoServicesContext(DbContextOptions<AutoServicesContext> options)
            : base(options) { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<FileToDatabase> FileToApis { get; set; }
        public DbSet<FileToDatabase> FileToDatabases { get; set; }
    }
}
