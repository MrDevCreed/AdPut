using AppService.Dto.Requests.Ad;
using AppService.Dto.Responses.Ad;
using System.Threading.Tasks;

namespace AppService.Interfaces
{
    public interface IAdService
    {
        CreateAdResponse Create(CreateAdRequest request);

        EditAdResponse Edit(EditAdRequest request);

        Task<FindAdResponse> Find(int id);

        GetAdFilteredListResponse GetAdFilteredList(GetAdFilteredListRequest request);

        AcceptAdResponse Accept(int id);

        RejectAdResponse Reject(int id);

        DeleteAdResponse Delete(int id);
    }
}
