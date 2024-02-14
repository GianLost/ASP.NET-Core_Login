using ASP.NET_Core_Login.Database;
using ASP.NET_Core_Login.Models;
using ASP.NET_Core_Login.Keys;
using ASP.NET_Core_Login.Helper.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Login.Services;

public class UserServices : IUserServices
{
    private readonly LoginContext _database;
    private readonly ICryptography _cryptography;

    public UserServices(LoginContext database, ICryptography cryptography)
    {
        _database = database;
        _cryptography = cryptography;
    }

    public async Task<Users> UserRegister(Users user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        user.Password = _cryptography.EncryptKey(user.Password);
        user.SessionToken = SessionTokenGenerator.GenerateSessionToken();
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
