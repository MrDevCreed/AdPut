using AppService.Dto.Requests.User;
using AppService.Dto.Responses.Common;
using AppService.Dto.Responses.User;
using AppService.Interfaces;
using Data.Repositories.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using Presentaion.Dto.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppService.implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserService(IUserRepository userRepository,
                           UserManager<IdentityUser> userManager,
                           SignInManager<IdentityUser> signInManager)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                IdentityUser identityUser = await _userManager.FindByIdAsync(request.Id);

                if (identityUser == null)
                {
                    return new ChangePasswordResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "User Not Found!"
                    };
                }

                IdentityResult identityResult = await _userManager.ChangePasswordAsync(identityUser, request.OldPassword, request.NewPassword);

                if (!identityResult.Succeeded)
                {
                    return new ChangePasswordResponse()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = identityResult.Errors.First().Description,
                    };
                }

                User user = _userRepository.GetUserByUserId(request.Id);

                if (user == null)
                {
                    return new ChangePasswordResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "User Not Found!"
                    };
                }

                UserViewModel userViewModel = new UserViewModel()
                {
                    UserId = user.UserId,
                    UserName = identityUser.UserName,
                    PhoneNumber = identityUser.PhoneNumber,
                    Name = user.Name,
                };

                return new ChangePasswordResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = userViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new ChangePasswordResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new ChangePasswordResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public async Task<CreateUserResponse> Create(CreateUserRequest request)
        {
            try
            {
                IdentityUser identityUser = new IdentityUser()
                {
                    UserName = request.UserName,
                    PasswordHash = request.Password,
                    PhoneNumber = request.PhoneNumber,
                    PhoneNumberConfirmed = true,

                };

                IdentityResult identityResult = await _userManager.CreateAsync(identityUser);

                if (!identityResult.Succeeded)
                {
                    return new CreateUserResponse()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = identityResult.Errors.First().Description,
                    };
                }

                User user = new User(identityUser.Id, request.Name);

                _userRepository.Add(user);
                _userRepository.SaveChanges();

                UserViewModel userViewModel = new UserViewModel()
                {
                    UserId = user.UserId,
                    UserName = identityUser.UserName,
                    PhoneNumber = identityUser.PhoneNumber,
                    Name = request.Name,
                };

                return new CreateUserResponse()
                {
                    StatusCode = StatusCode.Created,
                    Data = userViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new CreateUserResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new CreateUserResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public async Task<EditUserResponse> Edit(EditUserRequest request)
        {
            try
            {
                IdentityUser identityUser = await _userManager.FindByIdAsync(request.Id);

                if (identityUser == null)
                {
                    return new EditUserResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "User Not Found!"
                    };
                }

                identityUser.PhoneNumber = request.PhoneNumber;

                await _userManager.UpdateAsync(identityUser);

                User user = _userRepository.GetUserByUserId(request.Id);

                if (user == null)
                {
                    return new EditUserResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "User Not Found!"
                    };
                }

                user.ChangeName(request.Name);

                _userRepository.Edit(user);
                _userRepository.SaveChanges();

                UserViewModel userViewModel = new UserViewModel()
                {
                    UserId = user.UserId,
                    UserName = identityUser.UserName,
                    PhoneNumber = identityUser.PhoneNumber,
                    Name = request.Name,
                };

                return new EditUserResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = userViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new EditUserResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new EditUserResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public async Task<FindUserResponse> Find(string id)
        {
            try
            {
                User user = _userRepository.GetUserByUserId(id);
                IdentityUser identityUser = await _userManager.FindByIdAsync(id);

                if (identityUser == null || user == null)
                {
                    return new FindUserResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "User Not Found!"
                    };
                }

                UserViewModel userViewModel = new UserViewModel()
                {
                    UserId = user.UserId,
                    UserName = identityUser.UserName,
                    PhoneNumber = identityUser.PhoneNumber,
                    Name = user.Name,
                };

                return new FindUserResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = userViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new FindUserResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new FindUserResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public async Task<SignInUserResponse> SignIn(string id)
        {
            try
            {
                IdentityUser identityUser = await _userManager.FindByIdAsync(id);

                if (identityUser == null)
                {
                    return new SignInUserResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "User Not Found!"
                    };
                }

                await _signInManager.SignInAsync(identityUser, false);

                User user = _userRepository.GetUserByUserId(id);

                if (user == null)
                {
                    return new SignInUserResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "User Not Found!"
                    };
                }

                UserViewModel userViewModel = new UserViewModel()
                {
                    UserId = user.UserId,
                    UserName = identityUser.UserName,
                    PhoneNumber = identityUser.PhoneNumber,
                    Name = user.Name,
                };

                return new SignInUserResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = userViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new SignInUserResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new SignInUserResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public async Task<SignOutUserResponse> SignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return new SignOutUserResponse()
                {
                    StatusCode = StatusCode.Ok,
                };

            }
            catch (ArgumentException ex)
            {
                return new SignOutUserResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new SignOutUserResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }
    }
}
