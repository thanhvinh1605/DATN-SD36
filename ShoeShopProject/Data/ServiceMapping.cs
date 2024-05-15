using Microsoft.AspNetCore.Mvc;
using ShoeShopProject.Models;
using ShoeShopProject.Services;
using ShoeShopProject.ViewModels;

namespace ShoeShopProject.Data
{
    /// <summary>
    /// Mapping các service
    /// </summary>
    public class ServiceMapping
    {
        private readonly DBContext _context;
        private readonly HttpContext _httpContext;

        public ServiceMapping(DBContext context, HttpContext httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Cấu hình dữ liệu cho header
        /// </summary>
        public void MappingHeader(Controller controller)
        {
            try
            {
                EmrSession emrSession = new EmrSession(_httpContext);
                bool checkLogin = false;
                if (emrSession != null && emrSession.userId > -1)
                {
                    User user = _context.Users.FirstOrDefault(x => x.Id == emrSession.userId);
                    controller.ViewBag.UserHeader = user;

                    if (user != null)
                    {
                        checkLogin = true;
                        CartService cartService = new CartService(_context);
                        List<CartDetails> listCartDetails = cartService.GetUserCartDetails(user.Id);
                        controller.ViewBag.HeaderCartDetails = listCartDetails;
                        
                    }
                }

                List<Category> categories = _context.Categories.ToList();

                controller.ViewBag.HeaderListCategories = categories;
                controller.ViewBag.CheckLogin = checkLogin;
                controller.ViewBag.EmrSession = emrSession;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Cấu hình dữ liệu cho header
        /// </summary>
        public void MappingHeaderAdmin(Controller controller)
        {
            try
            {
                AdminSession adminSession = new AdminSession(_httpContext);
                adminSession.adminID = 3;
                if (adminSession != null && adminSession.adminID >= 0)
                {
                    Admin admin = _context.Admins.FirstOrDefault(x => x.Id == adminSession.adminID);
                    if (admin != null)
                    {
                        Role role = _context.Roles.FirstOrDefault(x => x.Id == admin.RoleId);
                        
                        controller.ViewBag.AdminHeader = admin;
                        controller.ViewBag.RoleHeader = role;
                        controller.ViewBag.CheckLogin = true;
                        controller.ViewBag.AdminSession = adminSession;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
