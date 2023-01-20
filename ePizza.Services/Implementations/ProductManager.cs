using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ePizza.Entities.Concrete;
using ePizza.Entities.Dtos;
using ePizza.Repositories.Interfaces;
using ePizza.Services.Interfaces;
using ePizza.Shared.Utilities.Results.Abstract;
using ePizza.Shared.Utilities.Results.ComplexType;
using ePizza.Shared.Utilities.Results.Concrete;

namespace ePizza.Services.Implementations
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductManager(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<IDataResult<ProductDto>> GetAsync(int productId)
        {

            var category = await _productRepository.GetAsync(x => x.Id == productId);
            if (category != null)
            {

                return new DataResult<ProductDto>(ResultStatus.Success, new ProductDto
                {
                    Product = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {

                return new DataResult<ProductDto>(ResultStatus.Error, "Product Not Found", new ProductDto
                {
                    Product = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Product Not Found"
                });
            }

        }

        public async Task<IDataResult<ProductListDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            if (products.Count > -1)
            {

                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = products,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {

                return new DataResult<ProductListDto>(ResultStatus.Error, "Product not found", new ProductListDto
                {
                    Products = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Product not found"
                });
            }
        }

        public async Task<IDataResult<ProductDto>> AddAsync(ProductAddDto productAddDto)
        {
            var product = _mapper.Map<Product>(productAddDto);
            var productAdded = await _productRepository.AddAsync(product);
            return new DataResult<ProductDto>(ResultStatus.Success, "Product Added", new ProductDto
            {
                Product = productAdded,
                ResultStatus = ResultStatus.Success,
                Message = "Product Added"
            });
        }

        public async Task<IDataResult<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var oldProduct = await _productRepository.GetAsync(c => c.Id == productUpdateDto.Id);
            var product = _mapper.Map<ProductUpdateDto, Product>(productUpdateDto, oldProduct);
            var updatedProduct = await _productRepository.UpdateAsync(product);
            await _productRepository.SaveAsync();
            return new DataResult<ProductDto>(ResultStatus.Success, "Product Updated", new ProductDto()
            {
                Product = updatedProduct,
                ResultStatus = ResultStatus.Success,
                Message = "Product Updated"
            });
        }

        public async Task<IDataResult<ProductDto>> DeleteAsync(int categoryId)
        {
            var product = await _productRepository.GetAsync(c => c.Id == categoryId);
            if (product!=null)
            {
                var deletedCategory=  await _productRepository.DeleteAsync(product);
                await _productRepository.SaveAsync();
                return new DataResult<ProductDto>(ResultStatus.Success,"Product is Deleted", new ProductDto
                {
                    Product = deletedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message ="Product is deleted"
                });
            }
            return new DataResult<ProductDto>(ResultStatus.Error,"Product not found", new ProductDto
            {
                Product = null,
                ResultStatus = ResultStatus.Error,
                Message = "Product not found"
            });
        }
    }
}
