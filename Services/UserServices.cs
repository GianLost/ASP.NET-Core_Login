using ASP.NET_Core_Login.Database;
using ASP.NET_Core_Login.Models;
using ASP.NET_Core_Login.Keys;

namespace ASP.NET_Core_Login.Services;

public class UserServices : IUserServices
{
    private readonly LoginContext _database;

    public UserServices(LoginContext database)
    {
        _database = database;
    }

    public async Task<Users> UserRegister(Users user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        user.SessionToken = $"TOKEN DE SESSÃO {user.Password}";
        user.UserStats = UsersStatsEnum.ENABLE;
        user.RegisterDate = DateTime.Now;

        await _database.Users.AddAsync(user);
        await _database.SaveChangesAsync();

        return user;
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
