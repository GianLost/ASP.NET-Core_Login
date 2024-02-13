using ASP.NET_Core_Login.Helper.Messages;
using ASP.NET_Core_Login.Helper.Validation;
using ASP.NET_Core_Login.Models;
using ASP.NET_Core_Login.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Login.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserServices _userServices;
    private readonly IUserValidation _validation;

    public UsersController(ILogger<UsersController> logger, IUserServices userServices, IUserValidation validation)
    {
        _logger = logger;
        _userServices = userServices;
        _validation = validation;
    }

    public IActionResult Index() => View();

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(Users user, string confirmPass)
    {
        if (string.IsNullOrEmpty(confirmPass))
        {
            TempData["ErrorPass"] = FeedbackMessages.ConfirmPassword;
            return View(user);
        }    

        if(ModelState.IsValid)
        {
           List<(string FieldName, string Message)> duplicateErrors = await _validation.ValidateUserFields(user);

            foreach (var (FieldName, Message) in duplicateErrors)
                ModelState.AddModelError(FieldName, Message);

            if (ModelState.ErrorCount > 0)
                return View(user); 
                
            if(user.Password == null) throw new ArgumentNullException(nameof(user));
                if (!_validation.ValidatePassword(user.Password, confirmPass, this))
                    return View(user);

            TempData["SuccessMessage"] = FeedbackMessages.SuccessUserRegister;

            await _userServices.UserRegister(user);
            return RedirectToAction("Index", "Home");
        }

        TempData.Clear();
        return View(user);
    }
}