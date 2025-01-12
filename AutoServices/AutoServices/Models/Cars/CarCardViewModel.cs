namespace AutoServices.Models.Cars
{
    public class CarCardViewModel
    {
        public Guid? CarId { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public string ExistingFilePath { get; set; }
    }
}
