using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopStock.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class HomeController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/AccsesDenied")]
        public IActionResult AccsesDenied()
        {


            return Content("...AccsesDenied");
        }

    }
}
