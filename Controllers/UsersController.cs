using ASP.NET_Core_Login.Models;
using ASP.NET_Core_Login.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Login.Controllers;
public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserServices _userServices;

    public UsersController(ILogger<UsersController> logger, IUserServices userServices)
    {
        _logger = logger;
        _userServices = userServices;
    }

    public IActionResult Index() => View();

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(Users user)
    {
        await _userServices.UserRegister(user);
        return RedirectToAction("Index", "Home");
    }

}