using System;
using System.ComponentModel.DataAnnotations;

namespace Presentaion.Dto.ViewModels
{
    public class AdViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(2),MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public string TownName { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public AdStatus AdStatus { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public ImageViewModel Image { get; set; }
    }
}
