using ShoeShopProject.Data;
using ShoeShopProject.Models;

namespace ShoeShopProject.Services
{
    /// <summary>
    /// Serive quản lý property
    /// </summary>
    public class PropertyService
    {
        private readonly DBContext _context;

        public PropertyService(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy danh sách toàn bộ color
        /// </summary>
        /// <returns></returns>
        public List<Color> GetAllColors()
        {
            List<Color> listColors = new List<Color>();

            var result = _context.Colors.ToList();
            if (result != null && result.Count > 0)
            {
                foreach (var color in result)
                {
                    listColors.Add(color);
                }
            }
            return listColors;
        }

        /// <summary>
        /// Lấy danh sách toàn bộ size
        /// </summary>
        /// <returns></returns>
        public List<Size> GetAllSize()
        {
            List<Size> listSize = new List<Size>();

            var result = _context.Sizes.ToList();
            if (result != null && result.Count > 0)
            {
                foreach (var size in result)
                {
                    listSize.Add(size);
                }
            }
            return listSize;
        }

        /// <summary>
        /// Lấy danh sách toàn bộ brand
        /// </summary>
        /// <returns></returns>
        public List<Brand> GetAllBrand()
        {
            List<Brand> listBrand = new List<Brand>();

            var result = _context.Brands.ToList();
            if (result != null && result.Count > 0)
            {
                foreach (var brand in result)
                {
                    listBrand.Add(brand);
                }
            }
            return listBrand;
        }

        /// <summary>
        /// Lấy danh sách toàn bộ slider active
        /// </summary>
        /// <returns></returns>
        public List<Slider> GetAllSliderActive()
        {
            List<Slider> list = new List<Slider>();

            var result = _context.Sliders.Where(x => x.Status == true).ToList();
            if (result != null && result.Count > 0)
            {
                foreach (var slider in result)
                {
                    list.Add(slider);
                }
            }
            return list;
        }

        /// <summary>
        /// Get all payment
        /// </summary>
        /// <returns></returns>
        public List<Payment> GetAllPaymentMethod()
        {
            List<Payment> listPayment = _context.Payments.OrderBy(x => x.Id).ToList();
            return listPayment;
        }
    }
}
