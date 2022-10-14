using Microsoft.AspNetCore.Mvc;

namespace MiniMvc
{
    public class CowController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}