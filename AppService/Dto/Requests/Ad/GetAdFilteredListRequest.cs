namespace AppService.Dto.Requests.Ad
{
    public class GetAdFilteredListRequest
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string Name { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public int CategoryId { get; set; }

        public int AddressId { get; set; }

        public int UserId { get; set; }

        public AdStatus AdStatus { get; set; }

        public bool OnlyWithPhoto { get; set; }
    }
}
