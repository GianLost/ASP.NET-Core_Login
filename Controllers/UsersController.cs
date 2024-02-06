using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Login.Controllers;
public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() => View();

    public IActionResult Register() => View();

}