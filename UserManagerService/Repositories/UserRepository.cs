using System.Collections.Generic;
using System.Linq;
using UserManagerService.Models;
using UserManagerService.Repositories.Interfaces;

namespace UserManagerService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagerServiceContext _context;

        public UserRepository(UserManagerServiceContext context)
        {
            _context = context;
        }

        public List<User> GetAllActiveUsers()
        {
            return _context.Users.Where(x => x.Active).ToList();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool UpdateUserState(int userId, bool active)
        {
            var userToUpdate = _context.Users.Find(userId);
            if (userToUpdate != null)
            {
                _context.Users.Attach(userToUpdate);
                userToUpdate.Active = active;
                _context.Entry(userToUpdate).Property(x => x.Active).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteUser(int userId)
        {
            var userToDelete = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
