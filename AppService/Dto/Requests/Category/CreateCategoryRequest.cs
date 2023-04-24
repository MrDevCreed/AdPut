namespace AppService.Dto.Requests.Category
{
    public class CreateCategoryRequest
    {
        public string Title { get; set; }

        public int ParentCategoryId { get; set; }
    }
}
