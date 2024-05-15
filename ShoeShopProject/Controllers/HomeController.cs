using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.Services;
using System.Diagnostics;

namespace ShoeShopProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly DBContext _context;

        public HomeController(ILogger<HomeController> logger, DBContext context)
		{
			_logger = logger;
			_context = context;
        }

		/// <summary>
		/// Gọi đến màn hình home page
		/// </summary>
		/// <returns></returns>
		public IActionResult HomePage(int? page, List<int>? brand, List<int>? size, string? search, int? category, string? sort)
		{
			try
			{
				// Mapping header
				ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
				mapping.MappingHeader(this);

				// Khai báo service
				ProductService productService = new ProductService(_context);
				PropertyService propertyService = new PropertyService(_context);

                int _page;
                if (page == null || page.Value <= 0)
                {
                    _page = 1;
                }
                else
                {
                    _page = page.Value;
                }
                //Lấy tổng số sản phẩm theo tìm kiếm
                int productCount = productService.GetProductCount(brand, search, size, category);
                int totalPage;
                List<int> _brand = new List<int>();
                if (brand != null && brand.Count > 0)
                {
                    _brand = brand;
                }
                List<int> _size = new List<int>();
                if (size != null && size.Count > 0)
                {
                    _size = size;
                }
                //Lấy số trang
                if (productCount % 12 == 0)
                {
                    totalPage = productCount / 12;
                }
                else
                {
                    totalPage = productCount / 12 + 1;
                }

                List<Product> listProducts = productService.GetProductListPaging(_page, brand, search, size, category, sort);
                ViewBag.totalPage = totalPage;
                ViewBag.page = _page;
                ViewBag.gap = 2;

                // Lấy các thông tin hiển thị
                List<Color> listColors = propertyService.GetAllColors();
                List<Size> listSizes = propertyService.GetAllSize();
                List<Brand> listBrands = propertyService.GetAllBrand();
                List<Slider> listSliders = propertyService.GetAllSliderActive();
                // Truyền thông tin hiển thị vào view
                ViewBag.ListProducts = listProducts;
                ViewBag.ListColors = listColors;
                ViewBag.ListSizes = listSizes;
                ViewBag.ListBrands = listBrands;
                ViewBag.ListSliders = listSliders;
                ViewBag.ListChooseBrand = _brand;
                ViewBag.ListChooseSize = _size;
                ViewBag.SearchCodition = search;
                ViewBag.CategorySearch = category;

            }
            catch (Exception ex)
            {
				Console.WriteLine(ex.ToString());
            }

            return View();
		}
	}
}
