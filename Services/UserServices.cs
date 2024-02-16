using ASP.NET_Core_Login.Database;
using ASP.NET_Core_Login.Models;
using ASP.NET_Core_Login.Keys;
using ASP.NET_Core_Login.Helper.Authentication;

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
        user.UserStats = UsersStatsEnum.ENABLE;
        user.RegisterDate = DateTime.Now;

        await _database.Users.AddAsync(user);
        await _database.SaveChangesAsync();

        // Generate session token
        string sessionToken = SessionTokenGenerator.GenerateSessionToken();

        // Store session token in TokenSession table
        TokenSession tokenSession = new()
        {
            SessionToken = sessionToken,
            UserId = user.Id, // Assuming Id is auto-generated
            RegisterDate = DateTime.Now
        };

        await _database.Tokens.AddAsync(tokenSession);
        await _database.SaveChangesAsync();

        // Store encrypted session token in Users table
        user.SessionToken = _cryptography.EncryptKey(sessionToken);

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
