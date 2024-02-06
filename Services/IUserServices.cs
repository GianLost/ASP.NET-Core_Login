using ASP.NET_Core_Login.Models;

namespace ASP.NET_Core_Login.Services;

public interface IUserServices
{
    Task<Users> UserRegister(Users user);
    Task<ICollection<Users>> UserList();
    Task<Users> UserEdit(Users user);
    void UserDelete(int id);
    Users SearchForId(int id);
}
