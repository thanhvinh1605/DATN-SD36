using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ShoeShopProject.Models;
using ShoeShopProject.Data;
using ShoeShopProject.Services;
using System.Security.Principal;
using ShoeShopProject.ViewModels;

namespace ShoeShopProject.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly DBContext _context;

        public AccountController(ILogger<AccountController> logger, DBContext context)
        {
            _logger = logger;
            this._context = context;
        }

        /// <summary>
        /// admin đăng nhập
        /// </summary>
        /// <returns></returns>
        [Route("login-admin")]
        [AllowAnonymous]
        public IActionResult LoginAdmin()
        {
            return View();
        }

        /// <summary>
        /// Khách hàng đăng nhập
        /// </summary>
        /// <returns></returns>
        [Route("login")]
        [AllowAnonymous]
        public IActionResult LoginUser()
        {
            return View();
        }

        /// <summary>
        /// Profile người dùng
        /// </summary>
        /// <returns></returns>
        [Route("account-profile")]
        [Authorize]
        public IActionResult AccountProfile(int userID)
        {
            ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
            mapping.MappingHeader(this);

            if (userID >= 0)
            {
                User user = _context.Users.FirstOrDefault(x => x.Id == userID);
                if (user != null)
                {
                    ViewBag.User = user;
                }
            }

            return View();
        }

        [Route("UpdateUserProfile")]
        [Authorize]
        public IActionResult UpdateUserProfile(int userID, string userName, string userDOB, string userPhone, string userAddress, IFormFile userImg)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            int userLoginID = emrSession.userId;
            User user = _context.Users.FirstOrDefault(u => u.Id == userID);

            if (user != null && userLoginID == userID)
            {
                
                string usrImagePath = "";
                var imagePath = "";
                string oldImg = user.Image;
                if (userImg != null && userImg.Length > 0)
                {
                    // Đảm bảo thư mục tồn tại hoặc tạo mới nếu chưa có
                    imagePath = Path.Combine("wwwroot", "assets", "img", "account", "profile");
                    Directory.CreateDirectory(imagePath);

                    DateTime SaveDate = DateTime.Now;

                    string fileNameWithoutExtension = $"profile_{user.Id}_{SaveDate:yyyyMMdd_HHmmss}";
                    string fileExtension = Path.GetExtension(userImg.FileName);
                    string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                    // Save the image to the directory
                    imagePath = Path.Combine(imagePath, fileName);

                    usrImagePath = $"/assets/img/account/profile/{fileName}";
                    user.Image = usrImagePath;
                }

                user.Fullname = userName;
                user.Phone = userPhone;
                user.Address = userAddress;
                if (!String.IsNullOrEmpty(userDOB))
                {
                    user.Birthday = DateTime.Parse(userDOB);
                }
                AccountService accountService = new AccountService(_context);
                int status = accountService.UpdateUser(user);

                if (status >= 0)
                {
                    if (userImg != null && !string.IsNullOrEmpty(imagePath))
                    {
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            userImg.CopyTo(stream);
                        }
                        if (!string.IsNullOrEmpty(oldImg))
                        {
                            var oldImagePath = Path.Combine("wwwroot", "assets", "img", "account", "profile", Path.GetFileName(oldImg));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                    }
                }

                emrSession.userId = user.Id;
                emrSession.userEmail = user.Email;
                emrSession.userName = user.Fullname;
                emrSession.userImage = user.Image != null ? user.Image : Constants.DEFAULT_IMG_USER;
                emrSession.putSession(HttpContext);

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		[Route("UserOrderHistory")]
		[Authorize]
		public IActionResult UserOrderHistory(int userID)
        {
            ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
            mapping.MappingHeader(this);
            if (userID >= 0)
            {
                User user = _context.Users.FirstOrDefault(x => x.Id == userID);
                
                if (user != null)
                {
                    List<Order> listOrders = _context.Orders.Where(o => o.UserId == user.Id).ToList();

                    ViewBag.ListOrders = listOrders;
                    ViewBag.User = user;
                }
            }
            return View();
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="orderID"></param>
		/// <returns></returns>
		[Route("UserOrderDetails")]
        [Authorize]
		public IActionResult UserOrderDetails(int orderID)
		{
			ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
			mapping.MappingHeader(this);

			Order order = _context.Orders.FirstOrDefault(o => o.Id == orderID);
			if (order != null)
			{
				User user = _context.Users.FirstOrDefault(user => user.Id == order.UserId);
				OrderService orderService = new OrderService(_context);
				List<OrderItemDetails> orderItemDetails = orderService.GetListOrderItemDetails(orderID);

				ViewBag.Order = order;
				ViewBag.User = user;
				ViewBag.ListOrderItem = orderItemDetails;
				ViewBag.PaymentMethod = _context.Payments.FirstOrDefault(p => p.Id == order.PaymentMethod);
			}

			return View();
		}

		/// <summary>
		/// Đăng nhập bằng google
		/// </summary>
		/// <param name="ReturnUrl"></param>
		/// <returns></returns>
		[Route("google-login")]
        [AllowAnonymous]
        public IActionResult GoogleLogin(string ReturnUrl)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("LoginCallback", new {returnUrl = ReturnUrl })
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);

        }

        /// <summary>
        /// Sau khi đăng nhập bằng google
        /// </summary>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [Route("LoginCallback")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCallback(string ReturnUrl)
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                // Kiểm tra đăng nhập nếu không thành công thì thông báo lỗi
                var errorMessage = String.Empty;
                if (!result.Succeeded)
                {
                    errorMessage = "Đăng nhập không thành công! Vui lòng kiểm tra thông tin đăng nhập";
                    return RedirectToAction("Login", "Account", new { message = errorMessage });
                }
                // Lấy giá trị email, name, img của người dùng khi đăng nhập
            
                var idClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
                string idIdentity = idClaim != null ? idClaim.Value : String.Empty;
                // Kiểm tra claims
                if (String.IsNullOrEmpty(idIdentity))
                {
                    errorMessage = "Không thể đăng nhập bằng tài khoản của bạn. Vui lòng kiểm tra lại email";
                    return RedirectToAction("Login", "Account", new { message = errorMessage });

                } else {
                    // Khai báo biến
                    var emailClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
                    var nameClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
                    var imgUrlClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Uri);

                    string usrEmail = emailClaim != null ? emailClaim.Value : String.Empty;
                    string usrName = nameClaim != null ? nameClaim.Value : String.Empty;
                    string usrImage = imgUrlClaim != null ? imgUrlClaim.Value : String.Empty;
                    var user = new User();

                    if (!String.IsNullOrEmpty(usrEmail))
                    {
                        user = _context.Users.FirstOrDefault(x => x.Email == usrEmail);
                        // Nếu user chưa tồn tại thì add vào DB
                        if (user == null)
                        {
                            User newUser = new User
                            {
                                Email = usrEmail,
                                Fullname = usrName,
                                Address = String.Empty,
                                Phone = String.Empty,
                                Image = !String.IsNullOrEmpty(usrImage) ? usrImage : Constants.DEFAULT_IMG_USER,
                                Birthday = null
                            };
                            AccountService accountService = new AccountService(_context);
                            user = accountService.AddUser(newUser);
                        }

                        Cart cartInfo = _context.Carts.FirstOrDefault(c => c.UserId == user.Id);
                        if (cartInfo == null)
                        {
                            cartInfo = new Cart();
                            cartInfo.UserId = user.Id;
                            cartInfo.UpdateDate= DateTime.Now;
                            _context.Carts.Add(cartInfo);
                            _context.SaveChanges();
                        }

                        EmrSession emrSession = new EmrSession(HttpContext);
                        emrSession.userId = user.Id;
                        emrSession.userIdIdentity = idIdentity;
                        emrSession.userEmail = user.Email;
                        emrSession.userName = user.Fullname;
                        emrSession.userImage = user.Image != null ? user.Image : String.Empty;
                        emrSession.putSession(HttpContext);

                        if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl); // Chuyển hướng đến returnUrl
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // Redirect to Homepage
            return RedirectToAction("HomePage", "Home");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("LoginAdminCallBack")]
        public IActionResult LoginAdminCallBack(string username, string password)
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                Admin admin = _context.Admins.FirstOrDefault(ad => ad.Username == username && ad.Password == password);
                if (admin != null)
                {
                    AdminSession adminSession = new AdminSession(HttpContext);
                    adminSession.adminID = admin.Id;
                    adminSession.userName = admin.Username;
                    adminSession.password = admin.Password;
                    adminSession.fullName = admin.Fullname;
                    adminSession.userImage = admin.Image;
                    adminSession.role = admin.RoleId;
                    adminSession.putSession(HttpContext);

                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [Route("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            EmrSession emrSession = new EmrSession(HttpContext);
            emrSession.clearSession(HttpContext);
            AdminSession adminSession = new AdminSession(HttpContext);
            adminSession.clearSession(HttpContext);
            return RedirectToAction("HomePage", "Home");
        }
    }
}
