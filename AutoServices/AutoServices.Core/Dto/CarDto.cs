using Microsoft.AspNetCore.Http;

namespace AutoServices.Core.Dto
{
    public class CarDto
    {
        public Guid? Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToApiDto> FilesToApiDtos { get; set; }
            = new List<FileToApiDto>();
    }
}
