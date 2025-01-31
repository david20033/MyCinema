namespace MyCinema.ViewModels
{
    public class PaymentAnalyticsViewModel
    {
        public decimal? TotalAmount { get; set; }
        public decimal VipTicketAmount { get; set; }
        public decimal RegularTicketAmount { get; set; }
        public string? MovieTitle { get; set; }
        public string? CustomerEmail { get; set; }
        public DateTime? Date { get; set; }
    }
}
