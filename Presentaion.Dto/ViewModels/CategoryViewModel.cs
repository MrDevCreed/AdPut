using System.ComponentModel.DataAnnotations;

namespace Presentaion.Dto.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }
    }
}
