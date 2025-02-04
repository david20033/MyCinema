﻿namespace MyCinema.ViewModels
{
    public class SalonMovieTimelineViewModel
    {
        public int SalonNumber { get; set; }
        public DateTime MovieStartTime { get; set; }
        public DateTime MovieEndTime { get; set; }
        public int Left { get;set; }
        public int? Width { get;set; }
        public Guid MovieId { get; set; }
        public Guid SalonId { get; set; }
        public string? Title { get; set; }
        public int SalonCapacity { get; set; }
        public int ReservedSeatsCount { get; set; }
        public TimeSpan CinemaOpenTime{ get; set; }
        public TimeSpan CinemaCloseTime { get; set; }
    }
}
