using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SallesApp.ViewModel.Login;

namespace SallesApp.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _managerUser;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> managerUser, SignInManager<IdentityUser> signInManager)
        {
            _managerUser = managerUser;
            _signInManager = signInManager;
        }   
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
            if(!ModelState.IsValid)
                return View(loginVM);

            var user = await _managerUser.FindByEmailAsync(loginVM.Email);
            
            if (user != null) 
            {
                var loginAttempt = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

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

    }
}
