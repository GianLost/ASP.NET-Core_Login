using ASP.NET_Core_Login.Models;

namespace ASP.NET_Core_Login.Helper.Authentication.Session;

public interface IUserSession
{
    Task<Users?> GetSessionAsync();
    Task<Users?> SignInAsync(string login);
    Task UserCheckIn(Users user);
    void UserCheckOut();
}