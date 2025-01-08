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

        public CarsServices
            (
            AutoServicesContext context

            )
        {
            _context = context;
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
            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();
            return domain;
        }
    }
}
