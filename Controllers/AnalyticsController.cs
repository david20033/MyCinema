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

        public async Task<IActionResult> Index([FromQuery] string fromDate, [FromQuery] string toDate)
        {
            fromDate ??= DateTime.Now.AddDays(-7).ToString();
            toDate ??= DateTime.Now.AddDays(0).ToString();
            DateTime from = DateTime.Parse(fromDate);
            DateTime to = DateTime.Parse(toDate);
            var data = await _analyticsService.MapPaymentsForPeriodToPaymentAnalyticViewModel(from, to);
            return View(data);
        }
    }
}
