using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Core.Dto
{
    public class FileToApiDto
    {
        public Guid? Id { get; set; }
        public string? ExistingFilePath { get; set; }
        public Guid? CarId { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
    }
}
