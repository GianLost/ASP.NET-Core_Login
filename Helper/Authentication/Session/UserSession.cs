using System.Globalization;
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
    private readonly TimeSpan SessionTimeoutDuration = TimeSpan.FromMinutes(30);

    private const string SessionKey = "userCheckIn";
    private const string SessionTimeoutKey = "sessionTimeout";

    public UserSession(IHttpContextAccessor httpContext, LoginContext database, ICryptography cryptography)
    {
        _httpContext = httpContext;
        _database = database;
        _cryptography = cryptography;
    }

    public async Task<Users?> GetSessionAsync()
    {
        string? userSession = _httpContext.HttpContext?.Session.GetString(SessionKey);
        
        DateTime? sessionTimeout = GetSessionTimeout();

        if (string.IsNullOrEmpty(userSession) || sessionTimeout == null || sessionTimeout < DateTime.Now) 
        {
            UserCheckOut();
            return null;
        }

        // Renova o tempo limite da sessão
        SetSessionTimeout(DateTime.UtcNow.Add(SessionTimeoutDuration));

        Users? user = await JsonSerializer.DeserializeAsync<Users>(new MemoryStream(Encoding.UTF8.GetBytes(userSession)));

        if(user == null) 
            throw new ArgumentNullException(nameof(user));

        return user; 
    
    }

    public async Task<Users?> SignInAsync(string login)
    {
        if (string.IsNullOrEmpty(login)) 
            throw new ArgumentNullException(nameof(login));

        return await _database.Users.FirstOrDefaultAsync(x => x.Login != null && x.Login.Trim().ToUpper() == login.Trim().ToUpper()); 
    }

    public async Task UserCheckIn(Users user)
    {
        if (user == null || string.IsNullOrEmpty(user.SessionToken)) 
            throw new ArgumentNullException(nameof(user));
            
        string? tokenSession = await GetTokenAsync(user);

        if(string.IsNullOrEmpty(tokenSession)) 
            throw new ArgumentNullException(nameof(user));

        if (!ValidateToken(tokenSession, user.SessionToken)) 
            throw new InvalidOperationException("Invalid session token");

        string value = JsonSerializer.Serialize(user);

        // Define o tempo limite da sessão
        DateTime sessionTimeout = DateTime.Now.Add(SessionTimeoutDuration);
        SetSessionTimeout(sessionTimeout); // Aqui você define o tempo limite da sessão na sessão

        _httpContext.HttpContext?.Session.SetString(SessionKey, value);
        _httpContext.HttpContext?.Session.SetInt32("userType", Convert.ToInt32(user.UserType));
        _httpContext.HttpContext?.Session.SetInt32("userStats", Convert.ToInt32(user.UserStats)); 
    }
    
    public void UserCheckOut()
    {
        _httpContext.HttpContext?.Session.Remove(SessionKey);
        _httpContext.HttpContext?.Session.Remove(SessionTimeoutKey);
        _httpContext.HttpContext?.Session.Clear();
    }

    private async Task<string?> GetTokenAsync(Users user)
    {
        TokenSession? tokenSession = await _database.Tokens.FirstOrDefaultAsync(x => x.UserId == user.Id);
        return tokenSession?.SessionToken;
    }

    private bool ValidateToken(string token, string hashedToken)
    {
        return _cryptography.VerifyKeyEncrypted(token, hashedToken);
    }

    private DateTime? GetSessionTimeout()
    {
        string? sessionTimeout = _httpContext.HttpContext?.Session.GetString(SessionTimeoutKey);

        if (sessionTimeout != null && DateTime.TryParseExact(sessionTimeout, "yyyy-MM-ddTHH:mm:ss.fffffffZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime timeout))
            return timeout;
        
        return null;
    }
    
    private void SetSessionTimeout(DateTime timeout)
    {
        _httpContext.HttpContext?.Session.SetString(SessionTimeoutKey, timeout.ToString("o"));
    }

}
