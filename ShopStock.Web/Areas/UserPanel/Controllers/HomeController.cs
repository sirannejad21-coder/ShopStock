using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopStock.Web.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("userpanel")]
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }


       

    }
}
