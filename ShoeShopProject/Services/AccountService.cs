using ShoeShopProject.Data;
using ShoeShopProject.Models;

namespace ShoeShopProject.Services
{
    /// <summary>
    /// Serive quản lý tài khoản - user
    /// </summary>
    public class AccountService
    {
        private readonly DBContext _context;

        public AccountService(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Thêm người dùng mới
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges(true);
            return user.Id;
        }

    }
}
