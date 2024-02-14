using ASP.NET_Core_Login.Models;

namespace ASP.NET_Core_Login.Helper.Authentication.Session;

public interface IUserSession
{
    void UserCheckIn(Users users);
    void UserCheckOut();
    Task<bool> ValidateTokenAsync(Users users);
    Task<Users?> GetSessionAsync();
    Task<Users?> SignInAsync(string login);
}