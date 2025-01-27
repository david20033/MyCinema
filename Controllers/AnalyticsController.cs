using Microsoft.AspNetCore.Mvc;
using MyCinema.Services.IServices;

namespace MyCinema.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsService _analyticsService;
        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public async Task<IActionResult> Index([FromQuery] string startDate, [FromQuery] string endDate)
        {
            //DateTime start = DateTime.Parse(startDate);
            //DateTime end = DateTime.Parse(endDate);
            var data = await _analyticsService.MapPaymentsForPeriodToPaymentAnalyticViewModel(new DateTime(2025, 1, 1), new DateTime(2025, 2, 1));
            return View(data);
        }
    }
}
