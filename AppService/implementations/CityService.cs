using AppService.Dto.Requests.City;
using AppService.Dto.Responses.City;
using AppService.Dto.Responses.Common;
using AppService.Interfaces;
using Data.Repositories.Interfaces;
using Domain;
using Presentaion.Dto.DetailedViewModels;
using Presentaion.Dto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppService.implementations
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            this._cityRepository = cityRepository;
        }

        public CreateCityResponse Create(CreateCityRequest request)
        {
            try
            {
                if (_cityRepository.IsNameExists(request.Name))
                {
                    return new CreateCityResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "City Already Exist",
                  };
                }

                City city = new City(request.Name);

                _cityRepository.Add(city);
                _cityRepository.SaveChanges();

                CityViewModel cityViewModel = new CityViewModel()
                {
                    Id = city.Id,
                    Name = request.Name,
                };

                return new CreateCityResponse()
                {
                    StatusCode = StatusCode.Created,
                    Data = cityViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new CreateCityResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new CreateCityResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public DeleteCityResponse Delete(int id)
        {
            try
            {
                City city = _cityRepository.FindById(id);

                if (city == null)
                {
                    return new DeleteCityResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "City Not Found",
                    };
                }

                _cityRepository.Delete(city);
                _cityRepository.SaveChanges();

                CityViewModel cityViewModel = new CityViewModel()
                {
                    Id = city.Id,
                    Name = city.Name,
                };

                return new DeleteCityResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = cityViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new DeleteCityResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new DeleteCityResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public EditCityResponse Edit(EditCityRequest request)
        {
            try
            {
                City city = _cityRepository.FindById(request.Id);

                if (city == null)
                {
                    return new EditCityResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "City Not Exist",
                    };
                }

                city.ChangeName(request.Name);
                List<Town> towns = request.Towns.Select(P => new Town(P.Name)).ToList();
                city.ChangeTowns(towns);

                _cityRepository.Edit(city);
                _cityRepository.SaveChanges();

                CityViewModel cityViewModel = new CityViewModel()
                {
                    Id = city.Id,
                    Name = city.Name,
                };

                return new EditCityResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = cityViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new EditCityResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new EditCityResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public FindCityResponse Find(int id)
        {
            try
            {
                City city = _cityRepository.FindById(id);

                if (city == null)
                {
                    return new FindCityResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "City Not Exist",
                    };
                }

                List<TownViewModel> towns = city.Towns.Select(P => new TownViewModel()
                {
                    Id = P.Id,
                    Name = P.Name,
                }).ToList();

                DetailedCityViewModel detailedCityViewModel = new DetailedCityViewModel()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Towns = towns,
                };

                return new FindCityResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = detailedCityViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new FindCityResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new FindCityResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public GetListCityResponse GetList()
        {
            try
            {
                List<City> cities = _cityRepository.GetList();

                List<CityViewModel> cityViewModels = cities.Select(P => new CityViewModel()
                {
                    Id = P.Id,
                    Name = P.Name,
                }).ToList();

                return new GetListCityResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = cityViewModels,
                };

            }
            catch (ArgumentException ex)
            {
                return new GetListCityResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new GetListCityResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }
    }
}
