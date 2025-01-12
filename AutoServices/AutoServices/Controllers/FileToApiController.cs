using AutoServices.Data;
using AutoServices.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoServices.Controllers
{
    public class FileToApiController : Controller
    {
        private readonly AutoServicesContext _context;

        public FileToApiController(AutoServicesContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Query to get file information along with car details (Make, Model, Year)
            var files = await _context.FileToApis
                .Include(x => x.Car) // Include related car details
                .Select(x => new CarCardViewModel
                {
                    CarId = x.Car.Id,
                    Make = x.Car.Make,
                    Model = x.Car.Model,
                    Year = x.Car.Year,
                    ExistingFilePath = x.ExistingFilePath
                })
                .ToListAsync();

            // Pass the files with car details to the view
            return View(files);
        }
    }
}
