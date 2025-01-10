using AutoServices.Core.Domain;
using AutoServices.Core.Dto;
using AutoServices.Core.ServiceInterface;
using AutoServices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly AutoServicesContext _context;
        public FileServices
            (
                IHostEnvironment webHost,
                AutoServicesContext context
            )
        {
            _webHost = webHost;
            _context = context;
        }
        public void FilesToApi(CarDto dto, Car car)
        {
            if (!Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload\\"))
            {
                Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
            }
            foreach (var file in dto.Files)
            {
                string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    FileToApi path = new FileToApi
                    {
                        Id = Guid.NewGuid(),
                        ExistingFilePath = uniqueFileName,
                        CarId = car.Id
                    };
                    _context.FileToApis.AddAsync(path);
                }
            }
        }
        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            foreach (var dtosItem in dtos)
            {
                var imageId = await _context.FileToApis
                    .FirstOrDefaultAsync(x => x.ExistingFilePath == dtosItem.ExistingFilePath);
                var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\"
                    + imageId.ExistingFilePath;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                _context.FileToApis.Remove(imageId);
                await _context.SaveChangesAsync();
            }
            return null;
        }
        public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
        {
            var imageId = await _context.FileToApis
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\"
                + imageId.ExistingFilePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _context.FileToApis.Remove(imageId);
            await _context.SaveChangesAsync();
            return null;
        }
    }
}
