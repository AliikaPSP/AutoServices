﻿namespace AutoServices.Core.Domain
{
    public class Car
    {
        public Guid? Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public ICollection<FileToDatabase> FileToApis { get; set; }
    }
}
