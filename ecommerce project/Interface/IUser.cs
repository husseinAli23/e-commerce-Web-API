using ecommerce_project.Models;

namespace ecommerce_project.Interface
{
    public interface IUser
    {
        ICollection<User> getUsers();
        User GetUserByID (int id);
        User GetUserByUsername (string username);
        User GetUserByEmail (string email);
        bool UserExistsEmail (string email);
        bool UserExistsID(int id);

        bool UpdateUser(User user);

        bool CreateUser(User user);
        bool Save();

    }
}
