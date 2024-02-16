using ASP.NET_Core_Login.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Login.Controllers;

[LoggedinUserFilter]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() => View();
}
