using AutoServices.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AutoServices.Data
{
    public class AutoServicesContext : DbContext
    {
        public AutoServicesContext(DbContextOptions<AutoServicesContext> options)
            : base(options) { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }
    }
}
