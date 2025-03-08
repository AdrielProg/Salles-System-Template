using Microsoft.AspNetCore.Mvc;

namespace SallesApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
