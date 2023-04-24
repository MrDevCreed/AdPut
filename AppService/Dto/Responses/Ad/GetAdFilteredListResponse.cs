using AppService.Dto.Responses.Common;
using Presentaion.Dto.ViewModels;
using System.Collections.Generic;

namespace AppService.Dto.Responses.Ad
{
    public class GetAdFilteredListResponse : ResponseBase<List<AdViewModel>>
    {
        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public int RecordCount { get; set; }
    }
}
