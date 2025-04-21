using AccountAPI2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AccountAPI2.Data
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);
        Task<User> GetCurrentUser(int userId = 0, string username = "");
        Task<IList<User>> GetUsers();
        Task<bool> LoginUser(string username, string password);
    }
    public class UserSQLRepository : IUserRepository
    {
        private readonly ILogger<User> _logger;
        private readonly AppDbContext _context;

        public UserSQLRepository(ILogger<User> logger, AppDbContext context)
        {
            this._logger = logger;
            this._context = context;
        }
        public async Task<int> CreateUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                var newUserId = await _context.SaveChangesAsync();
                return newUserId;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Create User:" + ex);
            }
            return 0;
        }

        public async Task<User> GetCurrentUser(int userId = 0, string username = "")
        {
            try
            {
                User user = null;
                if(userId > 0)
                {
                    user = await _context.Users.FindAsync(userId);
                }
                else if(!username.IsNullOrEmpty())
                {
                    user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Getting User:" + ex);
            }
            return null;
        }
        public async Task<IList<User>> GetUsers()
        {
            var users =  await _context.Users.ToListAsync();
            return users;
        }
        public async Task<bool> LoginUser(string username, string password)
        {
            return await _context.Users.AnyAsync(r => r.Username == username && r.Password == password);
        }
    }
}
