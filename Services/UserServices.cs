using ASP.NET_Core_Login.Models;

namespace ASP.NET_Core_Login.Services;

public class UserServices : IUserServices
{
    public Task<Users> UserRegister(Users user)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Users>> UserList()
    {
        throw new NotImplementedException();
    }
    public Task<Users> UserEdit(Users user)
    {
        throw new NotImplementedException();
    }

    public void UserDelete(int id)
    {
        throw new NotImplementedException();
    }

    public Users SearchForId(int id)
    {
        throw new NotImplementedException();
    }
}
