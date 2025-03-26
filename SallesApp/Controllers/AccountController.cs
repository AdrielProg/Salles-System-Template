using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SallesApp.Services.Interfaces;
using SallesApp.ViewModel.Login;

namespace SallesApp.Controllers;
    
public class AccountController(UserManager<IdentityUser> managerUser, SignInManager<IdentityUser> signInManager, IEncryptionService encryptService) : Controller
{
    private readonly UserManager<IdentityUser> _managerUser = managerUser;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly IEncryptionService _encryptService = encryptService;

    [HttpGet]
    public IActionResult Login(string url) 
    {
        return View(new LoginViewModel
        {
            ReturnUrl = url
        });
        
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM) 
    {
        if(!loginVM.IsLoginValid())
            return View(loginVM);

        var user = await _managerUser.FindByEmailAsync(loginVM.Email);
        var encriptedPassword = _encryptService.Encrypt(loginVM.Password);

        if (user != null) 
        {
            var loginAttempt = await _signInManager.PasswordSignInAsync(user, encriptedPassword, false, false);

            if (loginAttempt.Succeeded) 
            {
                if (!string.IsNullOrEmpty(loginVM.ReturnUrl))
                        return Redirect(loginVM.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }
        }
        this.ModelState.AddModelError("", "Falha ao realizar login !!");
        return View(loginVM);
    }
    
    [HttpPost]
    public async Task<IActionResult> CheckEmailExists(string email)
    {
        var user = await _managerUser.FindByEmailAsync(email);
        return Json(user != null);
    }

    public IActionResult RenderPartialView(string viewName)
    {
        return PartialView($"_{viewName}");
    }

    [HttpGet]
    public IActionResult Register() => View(new LoginViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(LoginViewModel registerVM)
    {
        if(registerVM.IsRegisterValid())
        {
            var encriptedPassword = _encryptService.Encrypt(registerVM.Password);

            var user = new IdentityUser() {Email = registerVM.Email, UserName = registerVM.Email};
            var userCreated = await _managerUser.CreateAsync(user, encriptedPassword);

            if (userCreated.Succeeded)
                return RedirectToAction("Login", "Account");
        
            this.ModelState.AddModelError("Registro", "Falha ao realizar o cadastro !!");
        }
        return View(registerVM);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.User = null;
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}

