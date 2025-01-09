using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCinema.Data
{
    public class MoviePhoto
    {
        public Guid Id { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
