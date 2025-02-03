using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyCinema.Enums;

namespace MyCinema.Data
{
    public class AppSetting
    {
        [Key]
        public string Key { get; set; }
        [Required]
        public  string Value { get; set; }
        public string? Description { get; set; }
        [Required]
        public Enums.InputType InputType { get; set; }
    }
}
