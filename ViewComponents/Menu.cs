using System.Text;
using System.Text.Json;
using ASP.NET_Core_Login.Helper.Messages;
using ASP.NET_Core_Login.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Login.ViewComponents;
public class Menu : ViewComponent
{
    private readonly ILogger<Menu> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Menu(IHttpContextAccessor httpContextAccessor, ILogger<Menu> logger)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        string? userSession = _httpContextAccessor.HttpContext?.Session.GetString("userCheckIn");

        if (string.IsNullOrEmpty(userSession))
        {
            string? signInUrl = Url.Action("Logout", "Login");
            _logger.LogError(FeedbackMessages.ExpiredSession);
            return View("Redirect", signInUrl);
        }

        using MemoryStream memoryStream = new(Encoding.UTF8.GetBytes(userSession));
        Users? users = await JsonSerializer.DeserializeAsync<Users>(memoryStream);

        return View(users);
    }
}
