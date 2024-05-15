using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ShoeShopProject.ViewModels
{
    public class ProductManageView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Categories { get; set; }
        public string Brand { get; set; }
        public decimal price { get; set; }
        public int totalStock { get; set; }

    }
}
