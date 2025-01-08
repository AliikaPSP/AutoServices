using AutoServices.Core.Domain;
using AutoServices.Core.Dto;
using AutoServices.Core.ServiceInterface;
using AutoServices.Data;
using AutoServices.Models.Cars;
using Microsoft.AspNetCore.Mvc;

namespace AutoServices.Controllers
{
    public class CarsController : Controller
    {
        private readonly AutoServicesContext _context;
        private readonly ICarsServices _carsServices;
        public CarsController
            (
            AutoServicesContext context,
            ICarsServices carsServices
            )
        {
            _context = context;
            _carsServices = carsServices;
        }

        public IActionResult Index()
        {
            var result = _context.Cars
                .Select(x => new CarsIndexViewModel
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    Year = x.Year,
                });
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var cars = await _carsServices.DetailAsync(id);
            if (cars == null)
            {
                return View("Error");
            }
            var vm = new CarsDetailsViewModel();
            vm.Make = cars.Make;
            vm.Model = cars.Model;
            vm.Year = cars.Year;
            vm.CreatedAt = cars.CreatedAt;
            vm.ModifiedAt = cars.ModifiedAt;
            return View(vm);

        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _carsServices.DetailAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            var vm = new CarCreateUpdateViewModel();
            vm.Id = car.Id;
            vm.Make = car.Make;
            vm.Model = car.Model;
            vm.Year = car.Year;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;
            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Make = vm.Make,
                Model = vm.Model,
                Year = vm.Year,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
            };
            var result = await _carsServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
    }
}
