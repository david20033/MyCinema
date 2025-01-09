using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCinema.Data;
using MyCinema.Services;
using MyCinema.Services.IServices;

namespace MyCinema.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SalonController : Controller
    {
        private readonly ISalonService _salonService;
        public SalonController(ISalonService salonService)
        {
            _salonService = salonService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddSalon()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSalon(TheatreSalon salon, string EmptySeatsCoords)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _salonService.AddSalonAsync(salon, EmptySeatsCoords);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("SalonNumber", ex.Message);
                }
            }
            return RedirectToAction("AddSalon");
        }

        public async Task<IActionResult> Salons()
        {
            var salons = await _salonService.GetTheatreSalonsAsync();
            return View(salons);
        }
        public async Task<IActionResult> SalonDetails(Guid Id)
        {
            return View(await _salonService.GetTheatreSalonByIdAsync(Id));
        }
    }
}
