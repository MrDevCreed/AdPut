using System.ComponentModel.DataAnnotations;

namespace Presentaion.Dto.ViewModels
{
    public class AddressViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public  CityViewModel City { get; set; }

        [Required]
        public TownViewModel Town { get; set; }
    }
}
