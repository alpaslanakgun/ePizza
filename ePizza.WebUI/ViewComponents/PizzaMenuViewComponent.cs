using ePizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.WebUI.ViewComponents
{
    public class PizzaMenuViewComponent : ViewComponent
    {

        private readonly IProductService _productService;
        public PizzaMenuViewComponent(IProductService _productService)
        {
            _productService = _productService;
        }

        public IViewComponentResult Invoke()
        {
            var items = _productService.GetAllAsync();
            return View("~/Views/Shared/_PizzaMenu.cshtml", items);
        }
    }
}
