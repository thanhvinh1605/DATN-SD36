using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.ViewModels;
namespace ShoeShopProject.Services
{
    /// <summary>
    /// Serive quản lý giỏ hàng - cart
    /// </summary>
    public class CartService
    {
        private readonly DBContext _context;

        public CartService(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add product or update to cart
        /// </summary>
        /// <param name="productVariantId"></param>
        /// <param name="quantity"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int AddToCart(int productVariantId, int quantity, int userID)
        {
            int status = -1;
            decimal priceUnit = 0;
            // nếu đã có productdetails
            // Lấy ra
            ProductService productService = new ProductService(_context);
            ProductVariant productVariant = _context.ProductVariants.FirstOrDefault(pv => pv.Id == productVariantId);
            
            if (productVariant != null)
            {
                Cart cart = _context.Carts.FirstOrDefault(c => c.UserId == userID);
                if (cart == null)
                {
                    cart = new Cart();
                    cart.UserId = userID;
                    cart.UpdateDate = DateTime.Now;
                    _context.Carts.Add(cart);
                    status = _context.SaveChanges();
                }

                priceUnit = _context.Products.FirstOrDefault(p => p.Id  == productVariant.ProductId).Price;

                CartItem cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cart.Id && ci.ProductId == productVariantId);
                if (cartItem == null)
                {
                    cartItem = new CartItem();
                    cartItem.CartId = cart.Id;
                    cartItem.ProductId = productVariantId;
                    cartItem.Quantity = quantity;
                    cartItem.PriceAmount = cartItem.Quantity * priceUnit;
                    _context.CartItems.Add(cartItem);
                    status = _context.SaveChanges();
                } 
                else
                {
                    cartItem.Quantity += quantity;
                    cartItem.PriceAmount = cartItem.Quantity * priceUnit;
                    _context.CartItems.Update(cartItem);
                    status = _context.SaveChanges();
                }
            }

            return status;
        }

        /// <summary>
        /// Get list cart items of user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<CartDetails> GetUserCartDetails(int userID)
        {
            List<CartDetails> listCartDetails = new List<CartDetails>();

            Cart cart = _context.Carts.FirstOrDefault(c => c.UserId == userID);
            if (cart != null)
            {
                var result = (from pv in _context.ProductVariants
                              join ci in _context.CartItems on pv.Id equals ci.ProductId
                              join p in _context.Products on pv.ProductId equals p.Id
                              join c in _context.Colors on pv.ColorId equals c.Id
                              join s in _context.Sizes on pv.SizeId equals s.Id
                              where ci.CartId == cart.Id
                              select new CartDetails
                              {
                                  cartItemId = ci.Id,
                                  productId = p.Id,
                                  productVariantId = pv.Id,
                                  productName = p.Name,
                                  colorName = c.Cname,
                                  size = s.SizeVal,
                                  unitPrice = p.Price,
                                  thumbnail = p.Thumbnail,
                                  quantity = ci.Quantity,
                                  priceAmout = ci.PriceAmount
                              }).ToList();

                if (result != null & result.Count > 0)
                {
                    listCartDetails.AddRange(result);
                }
            }
            return listCartDetails;
        }

        /// <summary>
        /// Delete cart item
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
        public int DeleteCartItem(int cartItemId)
        {
            int status = -1;
            CartItem cartItem = _context.CartItems.FirstOrDefault(c => c.Id == cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
                status = 0;
            }
            return status;
        }

        /// <summary>
        /// Change quantity cart items
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public int UpdateCartItemQuantity(int cartItemId, int productId, int quantity)
        {
            int status = -1;

            CartItem cartItem = _context.CartItems.FirstOrDefault(c => c.Id == cartItemId);
            Product product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (cartItem != null && product != null)
            {
                cartItem.Quantity = quantity;
                cartItem.PriceAmount = product.Price * quantity;
                _context.CartItems.Update(cartItem);
                _context.SaveChanges();
                status = 0;
            }
            return status;
        }


    }
}
