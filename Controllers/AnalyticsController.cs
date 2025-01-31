using Microsoft.AspNetCore.Mvc;
using MyCinema.Services.IServices;
using MyCinema.ViewModels;
using X.PagedList;
using X.PagedList.Extensions;

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
            var data = await _analyticsService.MapPaymentsForPeriodToAnalyticsIndexViewModel(from, to);
            return View(data);
        }
        public async Task<IActionResult> Movie()
        {
            var model = await _analyticsService.GetAnalyticsMovieViewModels(DateTime.MinValue, DateTime.MaxValue);
            return View(model);
        } 
        public async Task<IActionResult> Payments(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var data = await _analyticsService.GetPaymentAnalyticsViewModelsAsync();
            IPagedList<PaymentAnalyticsViewModel> pagedList = data.ToPagedList(pageNumber, pageSize);
            return View(pagedList);
        }
    }
}
