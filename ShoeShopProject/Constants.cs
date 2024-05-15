namespace ShoeShopProject
{
    /// <summary>
    /// Lưu trữ thông tin biến toàn cục, biến môi trường của hệ thống
    /// </summary>
    public class Constants
    {
        public const string DEFAULT_IMG_USER = "/assets/img/illustrations/profiles/profile-2.png";

        public const int NOT_APPROVE_ORDER = 0;     // chưa xử lý
        public const int SUCCESS_ORDER = 1;     // đã hoàn thành
        public const int APPROVE_ORDER = 2;     // đang xử lý
        public const int CANCEL_ORDER = 3;     // đã hủy

        public const int ROLE_ADMIN = 1;
        public const int ROLE_SALE = 2;

        public const bool NOT_PAYMENT_ORDER = false;     // chưa thanh toán
        public const bool PAYMENT_ORDER = true;     // đã thanh toán

        public const string PRO_CATEGORY = "category";
        public const string PRO_BRAND = "brand";
        public const string PRO_SIZE = "size";
        public const string PRO_COLOR = "color";

        /// <summary>
        /// Convert currency
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string ConvertCurrency(decimal amount)
        {
            return amount.ToString("#,##0₫");
        }
    }
}
