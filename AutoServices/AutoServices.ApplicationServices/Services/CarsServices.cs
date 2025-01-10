using AutoServices.Core.Domain;
using AutoServices.Core.ServiceInterface;
using AutoServices.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServices.Core.Dto;

namespace AutoServices.ApplicationServices.Services
{
    public class CarsServices : ICarsServices
    {
        private readonly AutoServicesContext _context;
        private readonly IFileServices _fileServices;

        public CarsServices
            (
            AutoServicesContext context,
            IFileServices fileServices

            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task <Car> DetailAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Car> Update(CarDto dto)
        {
            Car domain = new();
            domain.Id = dto.Id;
            domain.Make = dto.Make;
            domain.Model = dto.Model;
            domain.Year = dto.Year;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = DateTime.Now;
            _fileServices.FilesToApi(dto, domain);
            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();
            return domain;
        }

        public async Task<Car> Delete(Guid id)
        {
            var car = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            var images = await _context.FileToApis
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiDto
                {
                    Id = y.Id,
                    CarId = y.CarId,
                    ExistingFilePath = y.ExistingFilePath
                }).ToArrayAsync();
            await _fileServices.RemoveImagesFromApi(images);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> Create(CarDto dto)
        {
           Car car = new Car();
            car.Id = Guid.NewGuid();
            car.Make = dto.Make;
            car.Model = dto.Model;
            car.Year = dto.Year;
            car.CreatedAt = DateTime.Now;
            car.ModifiedAt = DateTime.Now;
            _fileServices.FilesToApi(dto, car);
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return car;
        }

    }
}
