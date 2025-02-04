﻿using System.ComponentModel.DataAnnotations;

namespace MyCinema.Data
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
