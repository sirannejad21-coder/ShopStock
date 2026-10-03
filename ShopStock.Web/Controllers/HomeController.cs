using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopStock.Web.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("aboutus")]
        public IActionResult AboutUs()
        {


            return View();
        }


        [Route("ContactUs")]
        public IActionResult ContactUs()
        {


            return View();
        }
    }
}
