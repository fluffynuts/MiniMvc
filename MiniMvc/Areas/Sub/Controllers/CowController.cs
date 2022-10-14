using Microsoft.AspNetCore.Mvc;

namespace MiniMvc.Areas.Sub.Controllers
{
    [Area("Sub")]
    public class CowController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}