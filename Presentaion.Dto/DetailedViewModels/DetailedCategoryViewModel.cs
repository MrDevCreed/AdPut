using Presentaion.Dto.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Presentaion.Dto.DetailedViewModels
{
    public class DetailedCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }

        public List<CategoryViewModel> SubCategories { get; set; }

        public CategoryViewModel ParentCategory { get; set; }
    }
}
