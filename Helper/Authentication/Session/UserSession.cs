using System.Text;
using System.Text.Json;
using ASP.NET_Core_Login.Database;
using ASP.NET_Core_Login.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Login.Helper.Authentication.Session;

public class UserSession : IUserSession
{
    private readonly LoginContext _database;
    private readonly IHttpContextAccessor _httpContext;

    public UserSession(IHttpContextAccessor httpContext, LoginContext database)
    {
        _httpContext = httpContext;
        _database = database;
    }

    public async Task<bool> ValidateTokenAsync(Users user)
    {
        if (user == null || string.IsNullOrEmpty(user.Login))
            return false;

        Users? searchUser = await _database.Users.FirstOrDefaultAsync(x => x.Login != null && x.Login.ToUpper() == user.Login.ToUpper());

        string? sessionToken = user.SessionToken;

        if (user != null && user.SessionToken == sessionToken)
            return true;
        
        return false;
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
        if (user.Login == null || string.IsNullOrEmpty(user.SessionToken)) throw new ArgumentNullException(nameof(user));

        if (ValidateTokenAsync(user).Result)
        {
            string value = JsonSerializer.Serialize(user);

            _httpContext?.HttpContext?.Session.SetString("userCheckIn", value);

            if (user.UserType != null)
                _httpContext?.HttpContext?.Session.SetInt32("userType", Convert.ToInt32(user.UserType));
                _httpContext?.HttpContext?.Session.SetInt32("userStats", Convert.ToInt32(user.UserStats)); 
        }
        else
        {
            throw new InvalidOperationException("Invalid session token");
        }
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
