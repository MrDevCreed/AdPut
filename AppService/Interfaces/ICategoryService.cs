using AppService.Dto.Requests.Category;
using AppService.Dto.Responses.Category;

namespace AppService.Interfaces
{
    public interface ICategoryService
    {
        CreateCategoryResponse Create(CreateCategoryRequest request);

        EditCategoryResponse Edit(EditCategoryRequest request);

        DeleteCategoryResponse Delete(int id);

        FindCategoryResponse Find(int id);

        GetListCategoryResponse GetList();
    }
}
