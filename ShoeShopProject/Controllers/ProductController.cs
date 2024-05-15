using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.Services;
using ShoeShopProject.ViewModels;

namespace ShoeShopProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly DBContext _context;

        public ProductController(ILogger<ProductController> logger,DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Product details
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public IActionResult ProductDetails(int productID)
        {
            ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
            mapping.MappingHeader(this);

            ProductService productService = new ProductService(_context);

            if(productID >= 0)
            {
                Product product = productService.GetProductById(productID);
                List<ProductDetails> listProductDetails = productService.GetListProductVariant(productID);
                List<Product> listProductSame = productService.ListProductsSameType(productID);
                if (product != null)
                {
                    ViewBag.Product = product;
                    ViewBag.ListProductDetails = listProductDetails;
                    ViewBag.ListProductSame = listProductSame;
                }
            }

            return View();
        }

        public IActionResult OderDetails()
        {
            ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
            mapping.MappingHeader(this);
            return View();
        }
    }
}
