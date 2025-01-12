using AutoServices.Core.Domain;
using AutoServices.Core.Dto;

namespace AutoServices.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(CarDto dto, Car car);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);
        Task<FileToApi> RemoveImageFromApi(FileToApiDto dtos);
    }
}
