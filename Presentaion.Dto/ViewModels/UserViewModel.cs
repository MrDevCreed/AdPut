using System.ComponentModel.DataAnnotations;

namespace Presentaion.Dto.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(3),MaxLength(50)]
        public string Name { get; set; }
    }
}
