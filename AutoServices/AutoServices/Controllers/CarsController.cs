using AutoServices.Core.Dto;
using AutoServices.Core.ServiceInterface;
using AutoServices.Data;
using AutoServices.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoServices.Controllers
{
    public class CarsController : Controller
    {
        private readonly AutoServicesContext _context;
        private readonly ICarsServices _carsServices;
        private readonly IFileServices _fileServices;
        public CarsController
            (
            AutoServicesContext context,
            ICarsServices carsServices,
            IFileServices fileServices
            )
        {
            _context = context;
            _carsServices = carsServices;
            _fileServices = fileServices;
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
                return NotFound();
            }
            var photos = await _context.FileToDatabases
                .Where(x => x.CarId == id)
                .Select(y => new ImageViewModel
                {
                    CarId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new CarsDetailsViewModel();

            vm.Id = cars.Id;
            vm.Make = cars.Make;
            vm.Model = cars.Model;
            vm.Year = cars.Year;
            vm.CreatedAt = cars.CreatedAt;
            vm.ModifiedAt = cars.ModifiedAt;
            vm.Image.AddRange(photos);
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


            var photos = await _context.FileToDatabases
              .Where(x => x.CarId == id)
              .Select(y => new ImageViewModel
              {
                  CarId = y.Id,
                  ImageId = y.Id,
                  ImageData = y.ImageData,
                  ImageTitle = y.ImageTitle,
                  Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
              }).ToArrayAsync();

            var vm = new CarCreateUpdateViewModel();
            vm.Id = car.Id;
            vm.Make = car.Make;
            vm.Model = car.Model;
            vm.Year = car.Year;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;
            vm.Image.AddRange(photos);
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
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        CarId = x.CarId,
                    }).ToArray()
            };
            var result = await _carsServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carsServices.DetailAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            var photos = await _context.FileToDatabases
                .Where(x => x.CarId == id)
                .Select(y => new ImageViewModel
                {
                    CarId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new CarDeleteViewModel();
            vm.Id = car.Id;
            vm.Make = car.Make;
            vm.Model = car.Model;
            vm.Year = car.Year;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;
            vm.Images.AddRange(photos);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var carId = await _carsServices.Delete(id);
            if (carId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel result = new();
            return View("CreateUpdate", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Make = vm.Make,
                Model = vm.Model,
                Year = vm.Year,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        CarId = x.CarId
                    }).ToArray()
            };
            var result = await _carsServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }

        public async Task<IActionResult> RemoveImage(ImageViewModel vm)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = vm.ImageId
            };
            var image = await _fileServices.RemoveImageFromDatabase(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
