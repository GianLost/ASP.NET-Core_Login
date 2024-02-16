using System.Text;
using System.Text.Json;
using ASP.NET_Core_Login.Database;
using ASP.NET_Core_Login.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Login.Helper.Authentication.Session;

public class UserSession : IUserSession
{
    private readonly LoginContext _database;
    private readonly ICryptography _cryptography;
    private readonly IHttpContextAccessor _httpContext;

    public UserSession(IHttpContextAccessor httpContext, LoginContext database, ICryptography cryptography)
    {
        _httpContext = httpContext;
        _database = database;
        _cryptography = cryptography;
    }

    public async Task<Users?> GetSessionAsync()
    {

        string? userSession = _httpContext?.HttpContext?.Session.GetString("userCheckIn");

        if (string.IsNullOrEmpty(userSession)) return null;

        Users? user = await JsonSerializer.DeserializeAsync<Users>(new MemoryStream(Encoding.UTF8.GetBytes(userSession)));

        if(user == null) throw new ArgumentNullException(nameof(user));
        return user; 
    
    }

    public void UserCheckIn(Users user)
    {
        if (user == null || string.IsNullOrEmpty(user.SessionToken)) throw new ArgumentNullException(nameof(user));

        TokenSession? tokenSession = _database.Tokens.FirstOrDefault(x => x.UserId == user.Id);

        if(string.IsNullOrEmpty(tokenSession?.SessionToken)) throw new ArgumentNullException(nameof(user));

        if (!ValidateToken(tokenSession.SessionToken, user.SessionToken)) throw new InvalidOperationException("Invalid session token");

        string value = JsonSerializer.Serialize(user);

        _httpContext?.HttpContext?.Session.SetString("userCheckIn", value);

        if (user.UserType != null)
            _httpContext?.HttpContext?.Session.SetInt32("userType", Convert.ToInt32(user.UserType));
            _httpContext?.HttpContext?.Session.SetInt32("userStats", Convert.ToInt32(user.UserStats)); 
    }

    public bool ValidateToken(string token, string hashedToken)
    {
        bool validateToken = _cryptography.VerifyKeyEncrypted(token, hashedToken);
        return validateToken;
    }

    public async Task<Users?> SignInAsync(string login)
    {
        if (string.IsNullOrEmpty(login)) 
            throw new ArgumentNullException(nameof(login));

        Users? user = await _database.Users.FirstOrDefaultAsync(x => x.Login != null && x.Login.ToUpper() == login.ToUpper());

        return user; 
    }
    
    public void UserCheckOut()
    {
        _httpContext?.HttpContext?.Session.Remove("userCheckIn");
        _httpContext?.HttpContext?.Session.Clear();
    }

}
