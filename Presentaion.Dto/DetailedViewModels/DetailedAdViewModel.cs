using Presentaion.Dto.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Presentaion.Dto.DetailedViewModels
{
    public class DetailedAdViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        [MinLength(3), MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public AddressViewModel Address { get; set; }

        [Required]
        public CategoryViewModel Category { get; set; }

        [Required]
        public AdStatus AdStatus { get; set; }

        [Required]
        public UserViewModel User { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public List<ImageViewModel> Images { get; set; }
    }
}
