using AutoServices.Core.Domain;
using AutoServices.Core.Dto;

namespace AutoServices.Core.ServiceInterface
{
    public interface IFileServices
    {
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);
        Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos);
        void UploadFilesToDatabase(CarDto dto, Car domain);
    }
}

