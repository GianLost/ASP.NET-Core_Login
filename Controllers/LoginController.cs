using ASP.NET_Core_Login.Helper.Authentication;
using ASP.NET_Core_Login.Helper.Authentication.Session;
using ASP.NET_Core_Login.Helper.Messages;
using ASP.NET_Core_Login.Helper.Validation;
using ASP.NET_Core_Login.Keys;
using ASP.NET_Core_Login.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Login.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUserSession _session;
    private readonly IUserValidation _validation;
    private readonly ICryptography _cryptography;

    public LoginController(ILogger<LoginController> logger, IUserSession session, IUserValidation validation, ICryptography cryptography)
    {
        _logger = logger;
        _session = session;
        _validation = validation;
        _cryptography = cryptography;
    }

    public async Task<IActionResult> Index()
    {
        if (await _session.GetSessionAsync() != null) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(Login login)
    {
        if (ModelState.IsValid)
        {

            if(string.IsNullOrEmpty(login.UserName)) throw new ArgumentNullException(nameof(login));
                Users? users = await _session.SignInAsync(login.UserName);
            
            if (!(users == null))
            {
                if (login.UserName != users.Login)
                {
                    _validation.LoginFieldsValidation("InvalidLogin", "O login informado é inválido", this);
                    return View("Index");
                }

                if (!_cryptography.VerifyKeyEncrypted(login.Password, users.Password))
                {
                    _validation.LoginFieldsValidation("InvalidPass", "A senha informada é inválida", this);
                    return View("Index");
                }

                if (users.UserStats != UsersStatsEnum.ENABLE)
                {
                    TempData["ErrorMessage"] = FeedbackMessages.ErrorAccountDisable;
                    return View("Index");
                }
                else
                {
                    _session.UserCheckIn(users);
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["ErrorMessage"] = FeedbackMessages.LogInDataNull;
        }

        return View("Index");
    }
    public IActionResult Logout()
    {
        _session.UserCheckOut();
        return RedirectToAction("Index", "Login");
    }
}
