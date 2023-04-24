using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Dto.Requests.Category
{
    public class EditCategoryRequest
    {
        public int Id { get; set; }

        public string Title{ get; set; }
    }
}
