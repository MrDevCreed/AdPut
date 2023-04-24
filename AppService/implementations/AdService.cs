using AppService.Dto.Requests.Ad;
using AppService.Dto.Responses.Ad;
using AppService.Dto.Responses.Common;
using AppService.Interfaces;
using Data.Repositories.Common;
using Data.Repositories.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using Presentaion.Dto.DetailedViewModels;
using Presentaion.Dto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.implementations
{
    public class AdService : IAdService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITownRepository _townRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IAdRepository _adRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public AdService(IAdRepository adRepository,
                         ICityRepository cityRepository,
                         ITownRepository townRepository,
                         ICategoryRepository categoryRepository,
                         IUserRepository userRepository,
                         UserManager<IdentityUser> userManager)
        {
            this._adRepository = adRepository;
            this._cityRepository = cityRepository;
            this._townRepository = townRepository;
            this._categoryRepository = categoryRepository;
            this._userRepository = userRepository;
            this._userManager = userManager;
        }

        public AcceptAdResponse Accept(int id)
        {
            try
            {
                Ad ad = _adRepository.FindById(id);

                if (ad == null)
                {
                    return new AcceptAdResponse()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = "Ad Not Found!",
                    };
                }

                ad.Accept();

                _adRepository.Edit(ad);
                _adRepository.SaveChanges();

                ImageViewModel imageViewModel = new ImageViewModel()
                {
                    Id = ad.Images.First().Id,
                    ImagePath = ad.Images.First().ImagePath,
                };

                AdViewModel adViewModel = new AdViewModel()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    Price = ad.Price,
                    Image = imageViewModel,
                    CityName = ad.Address.City.Name,
                    TownName = ad.Address.Town.Name,
                    AdStatus = ad.AdStatus,
                    CategoryName = _categoryRepository.FindById(ad.Category.Id).Title,
                    CreatedAt = ad.CreatedAt,
                };

                return new AcceptAdResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = adViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new AcceptAdResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new AcceptAdResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public CreateAdResponse Create(CreateAdRequest request)
        {
            try
            {
                City city = _cityRepository.FindById(request.Address.City.Id);
                Town town = _townRepository.FindById(request.Address.Town.Id);
                Address address = new Address(city, town);
                Category category = _categoryRepository.FindById(request.CategoryId);
                User user = _userRepository.FindById(request.UserId);

                Ad ad = new Ad(request.Name, request.Price, request.Description, address, category, user, request.AdStatus);

                if (request.Images != null)
                {
                    List<Image> images = request.Images.Select(P => new Image(P.ImagePath)).ToList();
                    ad.SetImages(images);
                }

                _adRepository.Add(ad);
                _adRepository.SaveChanges();

                ImageViewModel imageViewModel = new ImageViewModel()
                {
                    Id = ad.Images.First().Id,
                    ImagePath = ad.Images.First().ImagePath,
                };

                AdViewModel adViewModel = new AdViewModel()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    Price = ad.Price,
                    Image = imageViewModel,
                    CityName = ad.Address.City.Name,
                    TownName = ad.Address.Town.Name,
                    AdStatus = ad.AdStatus,
                    CategoryName = _categoryRepository.FindById(ad.Category.Id).Title,
                    CreatedAt = ad.CreatedAt,
                };

                return new CreateAdResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = adViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new CreateAdResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new CreateAdResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public DeleteAdResponse Delete(int id)
        {
            try
            {
                Ad ad = _adRepository.FindById(id);

                if (ad == null)
                {
                    return new DeleteAdResponse()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = "Ad Not Found!",
                    };
                }

                ad.Delete();

                _adRepository.Edit(ad);
                _adRepository.SaveChanges();

                CityViewModel cityViewModel = new CityViewModel()
                {
                    Id = ad.Address.City.Id,
                    Name = ad.Address.City.Name,
                };

                ImageViewModel imageViewModel = new ImageViewModel()
                {
                    Id = ad.Images.First().Id,
                    ImagePath = ad.Images.First().ImagePath,
                };

                AdViewModel adViewModel = new AdViewModel()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    Price = ad.Price,
                    Image = imageViewModel,
                    CityName = ad.Address.City.Name,
                    TownName = ad.Address.Town.Name,
                    AdStatus = ad.AdStatus,
                    CategoryName = _categoryRepository.FindById(ad.Category.Id).Title,
                    CreatedAt = ad.CreatedAt,
                };

                return new DeleteAdResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = adViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new DeleteAdResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new DeleteAdResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public EditAdResponse Edit(EditAdRequest request)
        {
            try
            {
                Ad ad = _adRepository.FindById(request.Id);

                if (ad == null)
                {
                    return new EditAdResponse()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = "Ad Not Found!",
                    };
                }

                City city = _cityRepository.FindById(request.CityId);
                Town town = _townRepository.FindById(request.TownId);
                Address address = ad.Address;

                address.ChangeCity(city);
                address.ChangeTown(town);

                List<Image> images = request.Images.Select(P => new Image(P.ImagePath)).ToList();

                ad.ChangeName(request.Name);
                ad.ChangePrice(request.Price);
                ad.ChangeDescription(request.Description);
                ad.ChangeAddress(address);
                ad.ChangeAdStatus(request.AdStatus);
                ad.SetImages(images);

                _adRepository.Edit(ad);
                _adRepository.SaveChanges();

                ImageViewModel imageViewModel = new ImageViewModel()
                {
                    Id = ad.Images.First().Id,
                    ImagePath = ad.Images.First().ImagePath,
                };

                AdViewModel adViewModel = new AdViewModel()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    Price = ad.Price,
                    Image = imageViewModel,
                    CityName = ad.Address.City.Name,
                    TownName = ad.Address.Town.Name,
                    AdStatus = ad.AdStatus,
                    CategoryName = _categoryRepository.FindById(ad.Category.Id).Title,
                    CreatedAt = ad.CreatedAt,
                };

                return new EditAdResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = adViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new EditAdResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new EditAdResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public async Task<FindAdResponse> Find(int id)
        {
            try
            {
                Ad ad = _adRepository.FindById(id);

                if (ad == null)
                {
                    return new FindAdResponse()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = "Ad Not Found!",
                    };
                }

                CityViewModel cityViewModel = new CityViewModel()
                {
                    Id = ad.Address.City.Id,
                    Name = ad.Address.City.Name,
                };

                TownViewModel townViewModel = new TownViewModel()
                {
                    Id = ad.Address.Town.Id,
                    Name = ad.Address.Town.Name,
                };

                AddressViewModel addressViewModel = new AddressViewModel()
                {
                    Id = ad.Address.Id,
                    City = cityViewModel,
                    Town = townViewModel,
                };

                CategoryViewModel categoryViewModel = new CategoryViewModel()
                {
                    Id = ad.Category.Id,
                    Title = ad.Category.Title,
                };

                List<ImageViewModel> imageViewModel = ad.Images.Select(P => new ImageViewModel()
                {
                    Id = P.Id,
                    ImagePath = P.ImagePath,
                }).ToList();

                IdentityUser identityUser = await _userManager.FindByIdAsync(ad.User.UserId);

                UserViewModel userViewModel = new UserViewModel()
                {
                    UserId = identityUser.Id,
                    Name = ad.User.Name,
                    UserName = identityUser.UserName,
                    PhoneNumber = identityUser.PhoneNumber,
                };

                DetailedAdViewModel adViewModel = new DetailedAdViewModel()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    Price = ad.Price,
                    Images = imageViewModel,
                    Address = addressViewModel,
                    AdStatus = ad.AdStatus,
                    Category = categoryViewModel,
                    CreatedAt = ad.CreatedAt,
                    Description = ad.Description,
                    User = userViewModel,
                };

                return new FindAdResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = adViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new FindAdResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new FindAdResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public GetAdFilteredListResponse GetAdFilteredList(GetAdFilteredListRequest request)
        {
            try
            {
                PageCollection<Ad> adPageCollection = _adRepository.GetFilteredList(request.PageSize,
                                                                                    request.PageNumber,
                                                                                    request.Name,
                                                                                    request.MinPrice,
                                                                                    request.MaxPrice,
                                                                                    request.CategoryId,
                                                                                    request.AddressId,
                                                                                    request.UserId,
                                                                                    request.AdStatus,
                                                                                    request.OnlyWithPhoto);

                if (adPageCollection == null)
                {
                    return new GetAdFilteredListResponse()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = "Ad Not Found!",
                    };
                }

                List<AdViewModel> adViewModels = adPageCollection.Data.Select(P => new AdViewModel()
                {
                    Id = P.Id,
                    Name = P.Name,
                    Price = P.Price,
                    Image = new ImageViewModel()
                    {
                        Id = P.Images.First().Id,
                        ImagePath = P.Images.First().ImagePath,
                    },
                    CityName = P.Address.City.Name,
                    TownName = P.Address.Town.Name,
                    AdStatus = P.AdStatus,
                    CategoryName = _categoryRepository.FindById(P.Category.Id).Title,
                    CreatedAt = P.CreatedAt,
                }).ToList();


                return new GetAdFilteredListResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = adViewModels,
                    PageCount = adPageCollection.PageCount,
                    PageSize = adPageCollection.PageSize,
                    RecordCount = adPageCollection.RecordCount,
                };

            }
            catch (ArgumentException ex)
            {
                return new GetAdFilteredListResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new GetAdFilteredListResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public RejectAdResponse Reject(int id)
        {
            try
            {
                Ad ad = _adRepository.FindById(id);

                if (ad == null)
                {
                    return new RejectAdResponse()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = "Ad Not Found!",
                    };
                }

                ad.Reject();

                _adRepository.Edit(ad);
                _adRepository.SaveChanges();

                ImageViewModel imageViewModel = new ImageViewModel()
                {
                    Id = ad.Images.First().Id,
                    ImagePath = ad.Images.First().ImagePath,
                };

                AdViewModel adViewModel = new AdViewModel()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    Price = ad.Price,
                    Image = imageViewModel,
                    CityName = ad.Address.City.Name,
                    TownName = ad.Address.Town.Name,
                    AdStatus = ad.AdStatus,
                    CategoryName = _categoryRepository.FindById(ad.Category.Id).Title,
                    CreatedAt = ad.CreatedAt,
                };

                return new RejectAdResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = adViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new RejectAdResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new RejectAdResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }
    }
}
