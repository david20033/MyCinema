namespace MyCinema.Data
{
    public class TicketOrder
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
