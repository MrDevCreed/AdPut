using System.ComponentModel.DataAnnotations;

namespace Presentaion.Dto.ViewModels
{
    public class ImageViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}
