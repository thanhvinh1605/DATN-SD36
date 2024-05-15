using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.Services;
using ShoeShopProject.ViewModels;

namespace ShoeShopProject.Controllers
{
    [Route("Shoping")]
    public class ShopingController : Controller
    {
        private readonly ILogger<ShopingController> _logger;
        private readonly DBContext _context;

        public ShopingController(ILogger<ShopingController> logger, DBContext context)
        {
            _logger = logger;
            _context=context;
        }

        [Route("Cart")]
        [Authorize]
        public IActionResult Cart()
        {
            ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
            mapping.MappingHeader(this);

            EmrSession emrSession = new EmrSession(HttpContext);
            int userID = emrSession.userId;
            if (userID >= 0)
            {
                Cart cartInfo = _context.Carts.FirstOrDefault(c => c.UserId == userID);
                if (cartInfo == null)
                {
                    cartInfo = new Cart();
                    cartInfo.UserId = userID;
                    cartInfo.UpdateDate= DateTime.Now;
                    _context.Carts.Add(cartInfo);
                    _context.SaveChanges();
                }
                ViewBag.CartInfo = cartInfo;

                CartService cartService = new CartService(_context);
                List<CartDetails> listCartDetails = cartService.GetUserCartDetails(userID);

                ViewBag.ListCartDetails = listCartDetails;
            }
            return View();
        }

        [Route("CartCheckout")]
        [Authorize]
        public IActionResult CartCheckout()
        {
            ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
            mapping.MappingHeader(this);

            // add product to cart
            EmrSession emrSession = new EmrSession(HttpContext);
            int userID = emrSession.userId;
            if (userID >= 0)
            {
                User user = _context.Users.FirstOrDefault(u => u.Id == userID);
                ViewBag.User = user;

                CartService cartService = new CartService(_context);
                List<CartDetails> listCartDetails = cartService.GetUserCartDetails(userID);
                ViewBag.ListCartDetails = listCartDetails;

                PropertyService propertyService = new PropertyService(_context);
                List<Payment> listPaymentMethod = propertyService.GetAllPaymentMethod();
                ViewBag.ListPaymentMethod = listPaymentMethod;
            }

            return View();
        }

        /// <summary>
        /// Order completion
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [Route("OrderCompletion")]
        [Authorize]
        public IActionResult OrderCompletion(int orderID)
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
        /// Cart Completion
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="totalAmount"></param>
        /// <param name="userName"></param>
        /// <param name="phone"></param>
        /// <param name="address"></param>
        /// <param name="orderNote"></param>
        /// <param name="paymentMethod"></param>
        /// <returns></returns>
        [Route("CartCompletion")]
        [Authorize]
        public IActionResult CartCompletion(int userID, int totalAmount, string userName, string phone, string address, string orderNote, int paymentMethod)
        {
            // add product to cart
            EmrSession emrSession = new EmrSession(HttpContext);
            int userLoginID = emrSession.userId;

            if (userLoginID == userID)
            {
                Cart cart = _context.Carts.FirstOrDefault(c => c.UserId == userID);
                if (cart != null)
                {
                    OrderService orderService = new OrderService(_context);
                    int orderID = orderService.CartCheckout(cart, userID, totalAmount, userName, phone, address, orderNote, paymentMethod);
                    if (orderID >= 0)
                    {
                        return Json(new { success = true, orderID = orderID });
                    }
                }
            }
            return Json(new { success = false });
        }

        [Route("AddToCart")]
        [Authorize]
        public IActionResult AddToCart(int shoeVariantId, int quantity)
        {
            // add product to cart
            EmrSession emrSession = new EmrSession(HttpContext);
            int userID = emrSession.userId;

            if (shoeVariantId >= 0)
            {
                CartService cartService = new CartService(_context);
                int status = cartService.AddToCart(shoeVariantId, quantity, userID);
                if (status < 0)
                {
                    return Json(new { success = false });
                }
            }
            return Json(new { success = true });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoeVariantId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [Route("DeleteCartItem")]
        [Authorize]
        public IActionResult DeleteCartItem(int cartItemId)
        {
            int status = -1;
            CartService cartService = new CartService(_context);

            status= cartService.DeleteCartItem(cartItemId);

            if (status < 0)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shoeVariantId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [Route("UpdateCartItemQuantity")]
        [Authorize]
        public IActionResult UpdateCartItemQuantity(int cartItemId, int productId, int quantity)
        {
            int status = -1;
            CartService cartService = new CartService(_context);
            status= cartService.UpdateCartItemQuantity(cartItemId, productId, quantity);

            if (status < 0)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
    }
}
