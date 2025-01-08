using AutoServices.Data;
using AutoServices.Models.Cars;
using Microsoft.AspNetCore.Mvc;

namespace AutoServices.Controllers
{
    public class CarsController : Controller
    {
        private readonly AutoServicesContext _context;
        public CarsController
            (
            AutoServicesContext context
            )
        {
            _context = context;
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
    }
}
