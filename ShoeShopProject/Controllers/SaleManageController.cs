using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.Services;
using ShoeShopProject.ViewModels;

namespace ShoeShopProject.Controllers
{
	[Route("SaleManage")]
	public class SaleManageController : Controller
    {
        private readonly ILogger<SaleManageController> _logger;
        private readonly DBContext _context;

        public SaleManageController(ILogger<SaleManageController> logger, DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("OrderManageList")]
        public IActionResult OrderManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            AdminSession adminSession = new AdminSession(HttpContext);
            OrderService orderService = new OrderService(_context);
            ViewBag.ListOrderManage = orderService.GetListOrderManage();
            ViewBag.AdminID = adminSession.adminID;

            return View();
        }

        /// <summary>
        /// Order completion
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [Route("OrderDetails")]
        public IActionResult OrderDetails(int orderID)
        {
			ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
			serviceMapping.MappingHeaderAdmin(this);

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
		/// 
		/// </summary>
		/// <param name="orderID"></param>
		/// <param name="paymentStatus"></param>
		/// <param name="orderStatus"></param>
		/// <returns></returns>
		[Route("UpdateOrder")]
		public IActionResult UpdateOrder(int orderID, bool paymentStatus, int orderStatus)
        {
            AdminSession adminSession = new AdminSession(HttpContext);
            if (orderID >= 0)
            {
                Order order = _context.Orders.FirstOrDefault(p => p.Id == orderID);
                if (order != null)
                {
					order.PaymentStatus = paymentStatus;
					order.OrderStatus = orderStatus;
					order.SaleId = adminSession.adminID;
					order.UpdateDate = DateTime.Now;
					_context.Orders.Update(order);
					_context.SaveChanges();

					if (paymentStatus == true && orderStatus == Constants.SUCCESS_ORDER)
                    {
                        List<OrderDetail> orderDetails = _context.OrderDetails.Where(o => o.OrderId == order.Id).ToList();
                        if (orderDetails != null && orderDetails.Count > 0)
                        {
                            foreach (OrderDetail detail in orderDetails)
                            {
                                ProductVariant product = _context.ProductVariants.FirstOrDefault(p => p.Id == detail.ProductId);
                                if (product != null && product.Quantity >= detail.Quantity)
                                {
                                    product.Quantity -= detail.Quantity;
                                    _context.ProductVariants.Update(product);
                                    _context.SaveChanges();
                                } 
                                else
                                {
									return Json(new { success = false, mess = true });
								}
                            }
                        }

                    }

					return Json(new { success = true });
				}
            }

			return Json(new { success = false, mess = false });
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[Route("MeOrderManageList")]
        public IActionResult MeOrderManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            AdminSession adminSession = new AdminSession(HttpContext);
            OrderService orderService = new OrderService(_context);
            ViewBag.ListOrderManage = orderService.GetListOrderManageByMe(adminSession.adminID);

            return View();
        }


    }
}
