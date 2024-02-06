using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Login.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() => View();
}
