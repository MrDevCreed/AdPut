using AppService.Dto.Requests.City;
using AppService.Dto.Responses.City;

namespace AppService.Interfaces
{
    public interface ICityService
    {
        CreateCityResponse Create(CreateCityRequest request);

        DeleteCityResponse Delete(int id);

        EditCityResponse Edit(EditCityRequest request);

        FindCityResponse Find(int id);

        GetListCityResponse GetList();
    }
}
