using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.ViewModels;

namespace ShoeShopProject.Services
{
    /// <summary>
    /// Serive quản lý thống kê
    /// </summary>
    public class DashboardService
    {
        private readonly DBContext _context;

        public DashboardService(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public decimal SumTotalAmount(DateTime stDate, DateTime endDate)
        {
            decimal totalAmount = 0;
            totalAmount = _context.Orders.Where(o => o.OrderStatus == Constants.SUCCESS_ORDER && o.UpdateDate >= stDate && o.UpdateDate <= endDate).Sum(o => o.TotalAmount);
            return totalAmount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public int SumTotalProductOrder(DateTime stDate, DateTime endDate)
        {
            int total = 0;

            var result = (from od in _context.Orders
                          join oi in _context.OrderDetails on od.Id equals oi.OrderId
                          where od.UpdateDate >= stDate && od.UpdateDate <= endDate
                          select oi.ProductId).ToList();

            if (result != null && result.Count > 0)
            {
                total = result.Sum();
            }

            return total;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderManageView> GetListLastestOrder()
        {
            List<OrderManageView> list = new List<OrderManageView>();
            var result = (from od in _context.Orders
                          join cus in _context.Users on od.UserId equals cus.Id
                          orderby od.Id descending
                          select new OrderManageView
                          {
                              orderId = od.Id,
                              customerName = cus.Fullname,
                              orderStatus = od.OrderStatus,
                              paymentStatus = od.PaymentStatus,
                              totalAmount = od.TotalAmount,
                              updateDate = od.UpdateDate,
                              saleID = od.SaleId,
                              saleName = String.Empty,
                          }).Take(7).ToList();

            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<decimal> GetListRevenueInYear()
        {
            DateTime currentDate = DateTime.Now;
            DateTime startOfYear = new DateTime(currentDate.Year, 1, 1);
            DateTime endOfYear = new DateTime(currentDate.Year, 12, 31);

            List<decimal> revenueList = new List<decimal>();

            for (int month = 1; month <= 12; month++)
            {
                DateTime startOfMonth = new DateTime(currentDate.Year, month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                decimal totalAmount = _context.Orders
                    .Where(o => o.OrderStatus == Constants.SUCCESS_ORDER && o.UpdateDate >= startOfMonth && o.UpdateDate <= endOfMonth)
                    .Sum(o => o.TotalAmount);

                revenueList.Add(totalAmount);
            }

            return revenueList;
        }
    }
}
