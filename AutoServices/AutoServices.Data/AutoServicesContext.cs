using AutoServices.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Data
{
    public class AutoServicesContext : DbContext
    {
        public AutoServicesContext(DbContextOptions<AutoServicesContext> options)
            : base(options) { }
        public DbSet<Car> Cars { get; set; }
    }
}
