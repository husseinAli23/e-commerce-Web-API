using ecommerce_project.Data;
using ecommerce_project.Interface;
using ecommerce_project.Models;

namespace ecommerce_project.Repository
{
    public class UserRepository : IUser
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public User GetUserByID(int id)
        {
            User? user = _context.Users.Where(user => user.ID == id).FirstOrDefault();
            return user;
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        public ICollection<User> getUsers()
        {
            return _context.Users.OrderBy(p => p.Username).ToArray();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserExistsEmail(string email)
        {
          return _context.Users.Any(_user => _user.Email == email);

        }

        public bool UserExistsID(int id)
        {
           return _context.Users.Any(_user => _user.ID == id);
        }
    }
}
