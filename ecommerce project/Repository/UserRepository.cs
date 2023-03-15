using ecommerce_project.Data;
using ecommerce_project.Interface;
using ecommerce_project.Models;
using Microsoft.AspNet.Identity;

namespace ecommerce_project.Repository;

public class AccountRepository : IAccount
{
    private readonly DatabaseContext _context;

    public AccountRepository(DatabaseContext context)
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
        return _context.Users.Where(x => x.Email == email).SingleOrDefault();
    }

    public User GetUserByID(int id)
    {
        User? user = _context.Users.Where(user => user.Id == id).SingleOrDefault();
        return user;
    }

    public User GetUserByUsername(string username)
    {
        return _context.Users.Where(u => u.UserName == username).SingleOrDefault();
    }

    public ICollection<User> getUsers()
    {
        return _context.Users.OrderBy(p => p.UserName).ToArray();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool UserExistsEmail(string email)
    {
      return _context.Users.Select(_user => _user.Email == email).SingleOrDefault();

    }

    public bool UserExistsID(int id)
    {
       return _context.Users.Select(_user => _user.Id == id).SingleOrDefault();
    }

    public string createPasswordHash(string password)
    {
        return new PasswordHasher().HashPassword(password);
    }
    public bool checkPasswordHash(string PasswordfromLogin, string PasswordfromDB)
    {
        var isCurrentHashValid = new PasswordHasher().VerifyHashedPassword(PasswordfromDB, PasswordfromLogin);
        if (isCurrentHashValid.ToString() == "Success")
        {
            return true;
        }
        return false;
    }
  
}
