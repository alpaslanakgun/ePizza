using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Entities.Dtos;
using ePizza.Repositories.Interfaces;
using ePizza.Services.Interfaces;
using ePizza.Shared.Utilities.Results.Abstract;
using ePizza.Shared.Utilities.Results.ComplexType;
using ePizza.Shared.Utilities.Results.Concrete;

namespace ePizza.Services.Implementations
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository _categoryRepository)
        {
            _categoryRepository = _categoryRepository;
        }
        public async Task<IDataResult<CategoryListDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories!=null)
            {

                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {

                return new DataResult<CategoryListDto>(ResultStatus.Error, "Category Not Found", new CategoryListDto
                {
                    Categories = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Category Not Found"
                });
            }

        }
    }
}
