using Microsoft.AspNetCore.Http;

namespace ShoeShopProject.Data
{
    public class AdminSession
    {
        /// <summary>
        /// Khởi tạo biến
        /// </summary>
        public int adminID { get; set; }
        public int role { get; set; }
        public string userName { get; set; } = "";
        public string fullName { get; set; } = "";
        public string password { get; set; } = "";
        public string userImage { get; set; } = "";

        /// <summary>
        /// Lấy thông tin session lưu trữ
        /// </summary>
        /// <param name="httpContext"></param>
        public AdminSession(HttpContext httpContext)
        {
            if (httpContext != null && httpContext.Session != null)
            {
                adminID = httpContext.Session.GetInt32("admin_id")??-1;
                role = httpContext.Session.GetInt32("role")??-1;
                password = string.IsNullOrEmpty(httpContext.Session.GetString("password")) ? "" : httpContext.Session.GetString("password");
                userName = string.IsNullOrEmpty(httpContext.Session.GetString("username")) ? "" : httpContext.Session.GetString("username");
                fullName = string.IsNullOrEmpty(httpContext.Session.GetString("fullname")) ? "" : httpContext.Session.GetString("fullname");
                userImage = string.IsNullOrEmpty(httpContext.Session.GetString("userimg")) ? "" : httpContext.Session.GetString("userimg");
            }
        }

        /// <summary>
        /// Thêm thông tin vào session
        /// </summary>
        /// <param name="httpContext"></param>
        public void putSession(HttpContext httpContext)
        {
            httpContext.Session.SetInt32("admin_id", adminID);
            httpContext.Session.SetInt32("role", role);
            httpContext.Session.SetString("password", password);
            httpContext.Session.SetString("username", userName);
            httpContext.Session.SetString("fullname", fullName);
            httpContext.Session.SetString("userimg", userImage);
        }

        /// <summary>
        /// Xóa session
        /// </summary>
        /// <param name="httpContext"></param>
        public void clearSession(HttpContext httpContext)
        {
            httpContext.Session.Clear();
        }
    }
}
