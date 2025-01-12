namespace AutoServices.Core.Domain
{
    public class FileToApi
    {
        public Guid Id { get; set; }
        public string? ExistingFilePath { get; set; }
        public Guid? CarId { get; set; }
        public Car Car { get; set; }

    }
}
