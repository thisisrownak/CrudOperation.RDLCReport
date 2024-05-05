using CrudOperation.Models.ViewModel;
using CrudOperation.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController (IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> AddProduct()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitProduct(List<ProductViewModel> products)
        {
            var result = await _productRepository.SubmitProduct(products);

            if (result.Success == true)
            {
                return Json(result.Message);
            }
            else
            {
                return Json(result.Message);
            }
        }
    }
}
