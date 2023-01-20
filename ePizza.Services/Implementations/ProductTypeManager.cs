
using System.Threading.Tasks;
using ePizza.Entities.Dtos;
using ePizza.Repositories.Interfaces;
using ePizza.Services.Interfaces;
using ePizza.Shared.Utilities.Results.Abstract;
using ePizza.Shared.Utilities.Results.ComplexType;
using ePizza.Shared.Utilities.Results.Concrete;

namespace ePizza.Services.Implementations
{
    public class ProductTypeManager:IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeManager(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        public async Task<IDataResult<ProductTypeListDto>> GetAllAsync()
        {
            var productTypes = await _productTypeRepository.GetAllAsync();
            if (productTypes != null)
            {

                return new DataResult<ProductTypeListDto>(ResultStatus.Success, new ProductTypeListDto
                { 
                    ProductTypes= productTypes,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {

                return new DataResult<ProductTypeListDto>(ResultStatus.Error, "Product Type Not Found", new ProductTypeListDto
                {
                    ProductTypes = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Product Not Found"
                });
            }
        }
    }
}
