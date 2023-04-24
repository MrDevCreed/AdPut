using Presentaion.Dto.ViewModels;
using System;
using System.Collections.Generic;

namespace AppService.Dto.Requests.Ad
{
    public class CreateAdRequest
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public AddressViewModel Address { get; set; }

        public int CategoryId { get; set; }

        public AdStatus AdStatus { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<ImageViewModel> Images { get; set; }
    }
}
