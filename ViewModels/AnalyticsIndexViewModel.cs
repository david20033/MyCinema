namespace MyCinema.ViewModels
{
    public class AnalyticsIndexViewModel
    {
        public List<PaymentAnalyticsViewModel> Analytics { get; set; } = [];
        public List<AnalyticsMovieViewModel> Movies { get; set; } = [];
    }
}
