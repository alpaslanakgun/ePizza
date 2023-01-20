using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ePizza.Entities.Concrete;
using ePizza.Entities.Dtos;
using ePizza.Repositories.Interfaces;
using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using ePizza.Shared.Utilities.Results.ComplexType;
using ePizza.WebUI.Helpers.Interfaces;
using ePizza.WebUI.Models;

namespace ePizza.WebUI.Area.Admin.Controllers
{
    public class ProductController : BaseController
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductTypeService _productTypeService;
        private readonly IFileHelper _fileHelper;

        public ProductController(IProductService productService, IFileHelper fileHelper, ICategoryService categoryService, IProductTypeService productTypeService)
        {
            _productService = productService;
            _fileHelper = fileHelper;
            _categoryService = categoryService;
            _productTypeService = productTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }

            return NotFound();


        }

        [HttpGet]
        public Task<IActionResult> Create()
        {
            ViewBag.Categories = _categoryService.GetAllAsync();
            ViewBag.ItemTypes = _productTypeService.GetAllAsync();
            return Task.FromResult<IActionResult>(View());
        }

        [HttpPost]

        public async Task<IActionResult> Create(ProductViewModel model)
        {

            if (!ModelState.IsValid)
            {

                model.ImageUrl = _fileHelper.UploadFile(model.File);
                ProductAddDto data = new ProductAddDto()
                {
                    Name = model.Name,
                    UnitPrice = model.UnitPrice,
                    CategoryId = model.CategoryId,
                    ProductTypeId = model.ProductTypeId,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl

                };
                var result = await _productService.AddAsync(data);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("",result.Message);
                }

            }

            ViewBag.Categories = _categoryService.GetAllAsync();
            ViewBag.ItemTypes = _categoryService.GetAllAsync();
            return View();




        }



    }
}
