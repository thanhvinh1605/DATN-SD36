using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Net.Mail;
using System.Net;
using ShoeShopProject.Models;
using ShoeShopProject.ViewModels;
namespace ShoeShopProject.Services
{
    /// <summary>
    /// Serive Mail
    /// </summary>
    public class MailServices
    {

        private readonly IConfiguration _configuration;
        public MailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendToEmail(string email, string title, Order order, List<OrderItemDetails> orderItemDetails)
        {

            string orderItemsHtml = string.Empty;

            if (orderItemDetails != null && orderItemDetails.Count > 0)
            {
                foreach (var item in orderItemDetails)
                {
                    orderItemsHtml += $@"
                    <tr>
                        <td><a href=""#"" style=""color: #3e637a;"">{item.productName}</a></td>
                        <td>{item.quantity}</td>
                        <td>{Constants.ConvertCurrency(item.priceAmout)}</td>
                    </tr>";
                }
            }

            string bodyHtml = $@"
            <html>
            <body>
                <h1>NEXUS SNEAKER</h1>
                <p>Cảm ơn bạn đã mua hàng NEXUS SNEAKER, thông tin đơn hàng:</p>
                <p><strong>Mã đơn hàng:</strong> {order.Id}</p>
                <p><strong>Tổng tiền:</strong> {Constants.ConvertCurrency(order.TotalAmount)}</p>
                <table border='1' cellpadding='5' cellspacing='0' width='100%'>
                    <thead>
                        <tr>
                            <th>Sản phẩm</th>
                            <th>Số lượng</th>
                            <th>Đơn giá</th>
                        </tr>
                    </thead>
                    <tbody>
                        {orderItemsHtml}
                    </tbody>
                </table>
            </body>
            </html>";

            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    UseDefaultCredentials = false,
                    Host = _configuration["SmtpSettings:Host"],
                    Port = int.Parse(_configuration["SmtpSettings:Port"]),
                    EnableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]),
                    Credentials = new NetworkCredential(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"])
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["SmtpSettings:Username"]),
                    Subject = title,
                    Body = bodyHtml,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Xử lý exception ở đây
            }
        }

    }
}
