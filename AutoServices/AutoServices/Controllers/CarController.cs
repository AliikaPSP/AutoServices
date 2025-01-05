using Microsoft.AspNetCore.Mvc;

namespace AutoServices.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
