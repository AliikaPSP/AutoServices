﻿namespace AutoServices.Models.Cars
{
    public class CarDeleteViewModel
    {
        public Guid? Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public List<ImageViewModel> Images { get; set; }
            = new List<ImageViewModel>();
    }
}
