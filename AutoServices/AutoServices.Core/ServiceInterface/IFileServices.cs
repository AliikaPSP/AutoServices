using AutoServices.Core.Domain;
using AutoServices.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(CarDto dto, Car car);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);
    }
}
