using AppService.Dto.Requests.Category;
using AppService.Dto.Responses.Category;
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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public CreateCategoryResponse Create(CreateCategoryRequest request)
        {
            try
            {
                if (_categoryRepository.IsTitleExists(request.Title))
                {
                    return new CreateCategoryResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "Category Already Exist",
                    };
                }

                Category parentcategory = _categoryRepository.FindById(request.ParentCategoryId);

                if (parentcategory == null)
                {
                    return new CreateCategoryResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "ParentCategory Not Found!",
                    };
                }

                Category category = new Category(request.Title, parentcategory);

                _categoryRepository.Add(category);
                _categoryRepository.SaveChanges();

                CategoryViewModel categoryViewModel = new CategoryViewModel()
                {
                    Id = category.Id,
                    Title = request.Title,
                };

                return new CreateCategoryResponse()
                {
                    StatusCode = StatusCode.Created,
                    Data = categoryViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new CreateCategoryResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new CreateCategoryResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public DeleteCategoryResponse Delete(int id)
        {
            try
            {
                Category category = _categoryRepository.FindById(id);

                if (category == null)
                {
                    return new DeleteCategoryResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "Category Not Found",
                    };
                }

                _categoryRepository.Delete(category);
                _categoryRepository.SaveChanges();

                CategoryViewModel categoryViewModel = new CategoryViewModel()
                {
                    Id = category.Id,
                    Title = category.Title,
                };

                return new DeleteCategoryResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = categoryViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new DeleteCategoryResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new DeleteCategoryResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public EditCategoryResponse Edit(EditCategoryRequest request)
        {
            try
            {
                Category category = _categoryRepository.FindById(request.Id);

                if (category == null)
                {
                    return new EditCategoryResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "Category Not Exist",
                    };
                }

                category.ChangeTitle(request.Title);

                _categoryRepository.Edit(category);
                _categoryRepository.SaveChanges();

                CategoryViewModel categoryViewModel = new CategoryViewModel()
                {
                    Id = category.Id,
                    Title = category.Title,
                };

                return new EditCategoryResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = categoryViewModel
                };

            }
            catch (ArgumentException ex)
            {
                return new EditCategoryResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new EditCategoryResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public FindCategoryResponse Find(int id)
        {
            try
            {
                Category category = _categoryRepository.FindById(id);

                if (category == null)
                {
                    return new FindCategoryResponse()
                    {
                        StatusCode = StatusCode.NotFound,
                        Message = "Category Not Exist",
                    };
                }

                CategoryViewModel parentCategory = new CategoryViewModel()
                {
                    Id = category.ParentCategory.Id,
                    Title = category.ParentCategory.Title,
                };

                List<CategoryViewModel> subCategories = category.SubCategories.Select(P => new CategoryViewModel()
                {
                    Id = P.Id,
                    Title = P.Title,
                }).ToList();

                DetailedCategoryViewModel detailedCategoryViewModel = new DetailedCategoryViewModel()
                {
                    Id = category.Id,
                    Title = category.Title,
                    ParentCategory = parentCategory,
                    SubCategories = subCategories,
                };

                return new FindCategoryResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = detailedCategoryViewModel,
                };

            }
            catch (ArgumentException ex)
            {
                return new FindCategoryResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new FindCategoryResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }

        public GetListCategoryResponse GetList()
        {
            try
            {
                List<Category> categories = _categoryRepository.GetBaseList();

                List<DetailedCategoryViewModel> detailedCategoryViewModels = categories.Select(P =>
                new DetailedCategoryViewModel()
                {
                    Id = P.Id,
                    Title = P.Title,
                    ParentCategory = new CategoryViewModel()
                    {
                        Id = P.ParentCategory.Id,
                        Title = P.ParentCategory.Title,
                    },
                    SubCategories = P.SubCategories.Select(O => new CategoryViewModel()
                    {
                        Id = O.Id,
                        Title = O.Title,
                    }).ToList(),
                }).ToList();

                return new GetListCategoryResponse()
                {
                    StatusCode = StatusCode.Ok,
                    Data = detailedCategoryViewModels,
                };

            }
            catch (ArgumentException ex)
            {
                return new GetListCategoryResponse()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch
            {
                return new GetListCategoryResponse()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Message = "Internal Server Error",
                };
            }
        }
    }
}
