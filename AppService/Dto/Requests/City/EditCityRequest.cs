using Presentaion.Dto.ViewModels;
using System.Collections.Generic;

namespace AppService.Dto.Requests.City
{
    public class EditCityRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TownViewModel> Towns { get; set; }
    }
}
