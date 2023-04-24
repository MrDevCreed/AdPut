using Presentaion.Dto.ViewModels;
using System.Collections.Generic;

namespace AppService.Dto.Requests.Ad
{
    public class EditAdRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public int CityId { get; set; }

        public int TownId { get; set; }

        public AdStatus AdStatus { get; set; }

        public List<ImageViewModel> Images { get; set; }
    }
}
