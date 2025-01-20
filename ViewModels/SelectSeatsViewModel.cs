namespace MyCinema.ViewModels
{
    public class SelectSeatsViewModel
    {
        public int SalonRows { get;set; }
        public int SalonColumns { get;set; }
        public List<string> EmptySeatsCoords { get; set; } = [];
        public List<string> ReservedSeatsCoords { get; set; } = [];
        public string SeatCoords { get; set; }
        public int TicketCount { get; set; }
        public Guid TicketOrderId { get; set; }
    }
}
