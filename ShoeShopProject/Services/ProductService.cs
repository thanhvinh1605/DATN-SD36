using Microsoft.EntityFrameworkCore;
using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.ViewModels;
using PagedList;
using PagedList.Mvc;
using System.Linq;

namespace ShoeShopProject.Services
{
    /// <summary>
    /// Serive quản lý sản phẩm - product
    /// </summary>
    public class ProductService
    {
        private readonly DBContext _context;

        public ProductService(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy danh sách toàn bộ product trong cửa hàng
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProducts()
        {
            List<Product> listProducts = new List<Product>();

            var result = _context.Products.ToList();
            if (result != null && result.Count > 0)
            {
                foreach (var product in result)
                {
                    listProducts.Add(product);
                }
            }

            return listProducts;
        }

        public int GetTotalProductInStock(int productID)
        {
            int total = 0;
            var result = (from p in _context.Products
                          join pv in _context.ProductVariants on p.Id equals pv.ProductId
                          where p.Id == productID
                          select pv.Quantity).Sum();
            if (result > 0)
            {
                total = result;
            }
            return total;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ProductManageView> GetListProductManage()
        {
            List<ProductManageView> productManages = new List<ProductManageView>();
            var result = (from p in _context.Products
                          join ca in _context.Categories on p.CategoryId equals ca.Id
                          join br in _context.Brands on p.BrandId equals br.Id
                          orderby p.Id
                          select new ProductManageView
                          {
                              Id = p.Id,
                              Image = p.Thumbnail,
                              Name = p.Name,
                              Brand = br.Name,
                              Categories = ca.Name,
                              price = p.Price,
                              totalStock = 0
                          }).ToList();

            if (result != null && result.Count > 0)
            {
                foreach (ProductManageView product in result)
                {
                    product.totalStock = GetTotalProductInStock(product.Id);
                    productManages.Add(product);
                }
            }

            return productManages;
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Get list all variant of product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ProductDetails> GetListProductVariant(int id)
        {
            List<ProductDetails> listProduct = new List<ProductDetails>();
            var result = (from pv in _context.ProductVariants
                          join p in _context.Products on pv.ProductId equals p.Id
                          join c in _context.Colors on pv.ColorId equals c.Id
                          join s in _context.Sizes on pv.SizeId equals s.Id
                          where pv.ProductId == id
                          orderby s.SizeVal, c.Cvalue
                          select new ProductDetails
                          {
                              pId = pv.Id,
                              colorID = c.Id,
                              colorVal = c.Cvalue,
                              colorName = c.Cname,
                              quantity = pv.Quantity,
                              sizeId = s.Id,
                              size = s.SizeVal
                          }).ToList();

            if (result != null && result.Count > 0)
            {
                foreach (var variant in result)
                {
                    listProduct.Add(variant);
                }
            }
            return listProduct;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Product> ListProductsSameType (int id)
        {
            List<Product> list = new List<Product>();
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);
            if( product != null)
            {
                var rs = _context.Products
                        .Where(x => x.BrandId == product.BrandId || x.CategoryId == product.CategoryId)
                        .Take(6)
                        .ToList();

                if (rs != null && rs.Any())
                {
                    list.AddRange(rs);
                }
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="search"></param>
        /// <param name="size"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public int GetProductCount(List<int>? brand, string? search, List<int>? size, int? category)
        {
            var res = _context.Products.Where(c => c.Id >= 0);

            if (search != null)
            {
                res = res.Where(s => s.Name.Contains(search));
            }
            if (brand != null && brand.Count > 0)
            {
                res = res.Where(s => brand.Any(b => b == s.BrandId));
            }
            if (category != null)
            {
                int currentCategoryId = category.Value;
                res = res.Where(s => s.CategoryId == currentCategoryId);
            }
            if (size != null && size.Count > 0)
            {
                res = res.Where(s => s.ProductVariants.Any(p => size.Contains(p.SizeId)));
            }

            var result = res.ToList();

            return result.Count;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="brand"></param>
        /// <param name="search"></param>
        /// <param name="size"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<Product> GetProductListPaging(int page, List<int>? brand, string? search, List<int>? size, int? category, string? sort)
        {
            var res = _context.Products.Where(c => c.Id >= 0);

            if (search != null)
            {
                res = res.Where(s => s.Name.Contains(search));
            }
            if (brand != null && brand.Count > 0)
            {
                res = res.Where(s => brand.Any(b => b == s.BrandId));
            }
            if (category != null)
            {
                int currentCategoryId = category.Value;
                res = res.Where(s => s.CategoryId == currentCategoryId);
            }
            if (size != null && size.Count > 0)
            {
                res = res.Where(s => s.ProductVariants.Any(p => size.Contains(p.SizeId)));
            }
            if (!String.IsNullOrEmpty(sort))
            {
                if ("asc".Equals(sort))
                {
                    res = res.OrderBy(s => s.Price);
                }
                else
                {
                    res = res.OrderByDescending(s => s.Price);
                }
            }

            List<Product> products = res.ToPagedList(page, 12).ToList();

            return products;
        }
    }
}
