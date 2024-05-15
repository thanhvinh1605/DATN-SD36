using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.Services;
using ShoeShopProject.ViewModels;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShoeShopProject.Controllers
{
    [Route("ProductManage")]
    public class ProductManageController : Controller
    {
        private readonly ILogger<ProductManageController> _logger;
        private readonly DBContext _context;

        public ProductManageController(ILogger<ProductManageController> logger, DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("ProductManageList")]
        public IActionResult ProductManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            ProductService productService = new ProductService(_context);
            List<ProductManageView> listProduct = productService.GetListProductManage();

            ViewBag.ProductList = listProduct;

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("AddProductManage")]
        public IActionResult AddProductManage()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            ViewBag.ListCategories = _context.Categories.ToList();
            ViewBag.ListBrand = _context.Brands.ToList();


            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("UpdateProductManage")]
        public IActionResult UpdateProductManage(int productID)
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);
            ProductService productService = new ProductService(_context);

            ViewBag.Product = _context.Products.FirstOrDefault(x => x.Id == productID);
            ViewBag.ListCategories = _context.Categories.ToList();
            ViewBag.ListBrand = _context.Brands.ToList();
            ViewBag.ProductVariantList = productService.GetListProductVariant(productID);
            ViewBag.ListColor = _context.Colors.ToList();
            ViewBag.ListSize = _context.Sizes.ToList();


            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCategory"></param>
        /// <param name="productBrand"></param>
        /// <param name="productName"></param>
        /// <param name="productPrice"></param>
        /// <param name="productDesc"></param>
        /// <param name="productImg"></param>
        /// <returns></returns>
        [Route("UpdateProductInfo")]
        public IActionResult UpdateProductInfo(int productID, int productCategory, int productBrand, string productName, int productPrice, string productDesc, IFormFile productImg)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == productID);
            if (product != null)
            {
                var imagePath = Path.Combine("wwwroot", "assets", "img", "products", "thumbnails");
                Directory.CreateDirectory(imagePath);

                string oldImg = product.Thumbnail;
                string imgThumbnails = String.Empty;

                if (productImg != null && productImg.Length > 0)
                {
                    string fileNameWithoutExtension = $"product_image_{DateTime.Now:yyyyMMdd_HHmmss}";
                    string fileExtension = Path.GetExtension(productImg.FileName);
                    string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                    // Save the image to the directory
                    imagePath = Path.Combine(imagePath, fileName);

                    imgThumbnails = $"/assets/img/products/thumbnails/{fileName}";
                    product.Thumbnail = imgThumbnails;
                }

                product.BrandId = productBrand;
                product.CategoryId = productCategory;
                product.Name = productName;
                product.Price = Convert.ToDecimal(productPrice);
                product.Desciption = productDesc;

                _context.Products.Update(product);
                _context.SaveChanges();

                if (productImg != null && !string.IsNullOrEmpty(imagePath))
                {
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        productImg.CopyTo(stream);
                    }
                    if (!string.IsNullOrEmpty(oldImg))
                    {
                        var oldImagePath = Path.Combine("wwwroot", "assets", "img", "products", "thumbnails", Path.GetFileName(oldImg));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }

                return Json(new { success = true });
            }
            
            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("DeleteProductInfo")]
        public IActionResult DeleteProductInfo(int productID)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id ==  productID);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCategory"></param>
        /// <param name="productBrand"></param>
        /// <param name="productName"></param>
        /// <param name="productPrice"></param>
        /// <param name="productDesc"></param>
        /// <param name="productImg"></param>
        /// <returns></returns>
       [Route("AddNewProduct")]
        public IActionResult AddNewProduct(int productCategory, int productBrand, string productName, int productPrice, string productDesc, IFormFile productImg)
        {

            if (productImg != null && productImg.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot", "assets", "img", "products", "thumbnails");
                Directory.CreateDirectory(imagePath);

                Product product = new Product();

                string oldImg = product.Thumbnail;
                string imgThumbnails = String.Empty;

                if (productImg != null && productImg.Length > 0)
                {
                    string fileNameWithoutExtension = $"product_image_{DateTime.Now:yyyyMMdd_HHmmss}";
                    string fileExtension = Path.GetExtension(productImg.FileName);
                    string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                    // Save the image to the directory
                    imagePath = Path.Combine(imagePath, fileName);

                    imgThumbnails = $"/assets/img/products/thumbnails/{fileName}";
                    product.Thumbnail = imgThumbnails;
                }

                product.BrandId = productBrand;
                product.CategoryId = productCategory;
                product.Name = productName;
                product.Price = Convert.ToDecimal(productPrice);
                product.Desciption = productDesc;

                _context.Products.Add(product);
                _context.SaveChanges();

                if (productImg != null && !string.IsNullOrEmpty(imagePath))
                {
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        productImg.CopyTo(stream);
                    }
                    if (!string.IsNullOrEmpty(oldImg))
                    {
                        var oldImagePath = Path.Combine("wwwroot", "assets", "img", "products", "thumbnails", Path.GetFileName(oldImg));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("PropertyManageList")]
        public IActionResult PropertyManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            ViewBag.ListCategories = _context.Categories.ToList();
            ViewBag.ListBrand = _context.Brands.ToList();
            ViewBag.ListColor = _context.Colors.ToList();
            ViewBag.ListSize = _context.Sizes.ToList();

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("DeleteProperty")]
        public IActionResult DeleteProperty(string property, int deleteID)
        {
            if (!String.IsNullOrEmpty(property) && deleteID >= 0)
            {
                if (Constants.PRO_CATEGORY.Equals(property))
                {
                    Category category = _context.Categories.FirstOrDefault(c => c.Id == deleteID);
                    if (category != null)
                    {
                        _context.Categories.Remove(category);
                        _context.SaveChanges();
                    }
                }
                else if (Constants.PRO_BRAND.Equals(property))
                {
                    Brand brand = _context.Brands.FirstOrDefault(c => c.Id == deleteID);
                    if (brand != null)
                    {
                        _context.Brands.Remove(brand);
                        _context.SaveChanges();
                    }
                }
                else if (Constants.PRO_SIZE.Equals(property))
                {
                    Size size = _context.Sizes.FirstOrDefault(c => c.Id == deleteID);
                    if (size != null)
                    {
                        _context.Sizes.Remove(size);
                        _context.SaveChanges();
                    }
                }
                else if (Constants.PRO_COLOR.Equals(property))
                {
                    Color color = _context.Colors.FirstOrDefault(c => c.Id == deleteID);
                    if (color != null)
                    {
                        _context.Colors.Remove(color);
                        _context.SaveChanges();
                    }
                }

                return Json(new { success = true });
            }
            
            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("UpdateProperty")]
        public IActionResult UpdateProperty(string property, int updateID, string propertyName)
        {
            if (!String.IsNullOrEmpty(property) && updateID >= 0 && !String.IsNullOrEmpty(propertyName))
            {
                if (Constants.PRO_CATEGORY.Equals(property))
                {
                    List<Category> categories = _context.Categories.Where(c => c.Id == updateID || c.Name.Equals(propertyName)).ToList();
                    if (categories != null && categories.Count == 1 && !propertyName.Equals(categories[0].Name))
                    {
                        categories[0].Name = propertyName;
                        _context.Categories.Update(categories[0]);
                        _context.SaveChanges();
                        return Json(new { success = true, updated = true });
                    }
                    else
                    {
                        return Json(new { success = true, updated = false });
                    }
                }
                else if (Constants.PRO_BRAND.Equals(property))
                {
                    List<Brand> brands = _context.Brands.Where(c => c.Id == updateID || c.Name.Equals(propertyName)).ToList();
                    if (brands != null && brands.Count == 1 && !propertyName.Equals(brands[0].Name))
                    {
                        brands[0].Name = propertyName;
                        _context.Brands.Update(brands[0]);
                        _context.SaveChanges();
                        return Json(new { success = true, updated = true });
                    }
                    else
                    {
                        return Json(new { success = true, updated = false });
                    }
                }
                else if (Constants.PRO_SIZE.Equals(property))
                {
                    int sizeVal = Convert.ToInt32(propertyName);
                    List<Size> sizes = _context.Sizes.Where(c => c.Id == updateID || c.SizeVal == sizeVal).ToList();
                    if (sizes != null && sizes.Count == 1 && sizeVal != sizes[0].SizeVal)
                    {
                        sizes[0].SizeVal = sizeVal;
                        _context.Sizes.Update(sizes[0]);
                        _context.SaveChanges();
                        return Json(new { success = true, updated = true });
                    }
                    else
                    {
                        return Json(new { success = true, updated = false });
                    }
                }
                else if (Constants.PRO_COLOR.Equals(property))
                {
                    List<Color> colors = _context.Colors.Where(c => c.Id == updateID || c.Cname.Equals(propertyName)).ToList();
                    if (colors != null && colors.Count == 1 && !propertyName.Equals(colors[0].Cname))
                    {
                        colors[0].Cname = propertyName;
                        _context.Colors.Update(colors[0]);
                        _context.SaveChanges();
                        return Json(new { success = true, updated = true });
                    }
                    else
                    {
                        return Json(new { success = true, updated = false });
                    }
                }
            }

            return Json(new { success = false });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("AddProperty")]
        public IActionResult AddProperty(string pType, string pVal)
        {
            if (!String.IsNullOrEmpty(pType) && !String.IsNullOrEmpty(pVal))
            {
                if (Constants.PRO_CATEGORY.Equals(pType))
                {
                    Category category = _context.Categories.FirstOrDefault(c => c.Name.Equals(pVal));
                    if (category == null)
                    {
                        category = new Category();
                        category.Name = pVal;
                        _context.Categories.Add(category);
                        _context.SaveChanges(true);
                        return Json(new { success = true });
                    }
                }
                else if (Constants.PRO_BRAND.Equals(pType))
                {
                    Brand brand = _context.Brands.FirstOrDefault(c => c.Name.Equals(pVal));
                    if (brand == null)
                    {
                        brand = new Brand();
                        brand.Name = pVal;
                        _context.Brands.Add(brand);
                        _context.SaveChanges(true);
                        return Json(new { success = true });
                    }
                }
                else if (Constants.PRO_SIZE.Equals(pType))
                {
                    Size size = _context.Sizes.FirstOrDefault(s => s.SizeVal == Convert.ToInt32(pVal));
                    if (size == null)
                    {
                        size = new Size();
                        size.SizeVal = Convert.ToInt32(pVal);
                        _context.Sizes.Add(size);
                        _context.SaveChanges(true);
                        return Json(new { success = true });
                    }
                }
                else if (Constants.PRO_COLOR.Equals(pType))
                {
                    Color color = _context.Colors.FirstOrDefault(c => c.Cname.Equals(pVal));
                    if (color == null)
                    {
                        color = new Color();
                        color.Cname = pVal;
                        color.Cvalue = pVal;
                        _context.Colors.Add(color);
                        _context.SaveChanges(true);
                        return Json(new { success = true });
                    }
                }
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [Route("AddProductVariant")]
        public IActionResult AddProductVariant(int productID, int size, int color, int quantity)
        {
            AdminSession adminSession = new AdminSession(HttpContext);
            if (productID >= 0 && size >= 0 && color >= 0 && quantity > 0)
            {
                Product product = _context.Products.FirstOrDefault(x => x.Id == productID);
                if (product != null)
                {
                    ProductVariant variant = _context.ProductVariants.FirstOrDefault(x => x.ProductId == product.Id && x.SizeId == size && x.ColorId == color);
                    if (variant == null)
                    {
                        variant = new ProductVariant();
                        variant.ProductId = product.Id;
                        variant.SizeId = size;
                        variant.ColorId = color;
                        variant.Quantity = quantity;
                        variant.UpdateDate = DateTime.Now;
                        variant.UpdateUser = adminSession.adminID;
                        _context.ProductVariants.Add(variant);
                        _context.SaveChanges();
                    }
                    else
                    {
                        variant.Quantity += quantity;
                        variant.UpdateDate = DateTime.Now;
                        variant.UpdateUser = adminSession.adminID;
                        _context.ProductVariants.Update(variant);
                        _context.SaveChanges();

                    }
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variantID"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [Route("UpdateProductVariant")]
        public IActionResult UpdateProductVariant(int variantID, int quantity)
        {
            AdminSession adminSession = new AdminSession(HttpContext);
            if (variantID >= 0 && quantity > 0)
            {
                ProductVariant productVariant = _context.ProductVariants.FirstOrDefault(x => x.Id == variantID);
                if (productVariant != null)
                {
                    productVariant.Quantity = quantity;
                    productVariant.UpdateDate = DateTime.Now;
                    productVariant.UpdateUser = adminSession.adminID;
                    _context.ProductVariants.Update(productVariant);
                    _context.SaveChanges();

                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("SliderManageList")]
        public IActionResult SliderManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            ViewBag.ListSlider = _context.Sliders.ToList();
            ViewBag.ListContact = _context.Contacts.ToList();

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        [Route("AddSlider")]
        public IActionResult AddSlider(IFormFile img)
        {
            var imagePath = Path.Combine("wwwroot", "assets", "img", "slider");
            Directory.CreateDirectory(imagePath);

            if (img != null && img.Length > 0)
            {
                Slider slider = new Slider();

                if (img != null && img.Length > 0)
                {
                    string fileNameWithoutExtension = $"slider_{DateTime.Now:yyyyMMdd_HHmmss}";
                    string fileExtension = Path.GetExtension(img.FileName);
                    string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                    // Save the image to the directory
                    imagePath = Path.Combine(imagePath, fileName);
                    slider.Image = $"/assets/img/slider/{fileName}";
                    slider.Status = true;
                    _context.Sliders.Add(slider);
                    _context.SaveChanges();

                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            img.CopyTo(stream);
                        }
                    }

                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sliderID"></param>
        /// <returns></returns>
        [Route("DeleteSlider")]
        public IActionResult DeleteSlider(int sliderID)
        {
            if (sliderID >= 0)
            {
                Slider slider = _context.Sliders.FirstOrDefault(x => x.Id ==  sliderID);
                if (slider != null)
                {
                    _context.Sliders.Remove(slider);
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sliderID"></param>
        /// <returns></returns>
        [Route("UpdateSlider")]
        public IActionResult UpdateSlider(int sliderID)
        {
            if (sliderID >= 0)
            {
                Slider slider = _context.Sliders.FirstOrDefault(x => x.Id ==  sliderID);
                if (slider != null)
                {
                    if (slider.Status == true)
                    {
                        slider.Status = false;
                    } else
                    {
                        slider.Status = true;
                    }
                    _context.Sliders.Update(slider);
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }
    }
}
