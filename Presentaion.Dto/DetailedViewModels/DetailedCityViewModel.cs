using Presentaion.Dto.ViewModels;
using System.Collections.Generic;

namespace Presentaion.Dto.DetailedViewModels
{
    public class DetailedCityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TownViewModel> Towns { get; set; }
    }
}
