using AppService.Dto.Requests.User;
using AppService.Dto.Responses.User;
using System.Threading.Tasks;

namespace AppService.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserResponse> Create(CreateUserRequest request);

        Task<EditUserResponse> Edit(EditUserRequest request);

        Task<FindUserResponse> Find(string id);

        Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request);

        Task<SignInUserResponse> SignIn(string id);

        //SignOut the Current User that Requested.
        Task<SignOutUserResponse> SignOut();
    }
}
