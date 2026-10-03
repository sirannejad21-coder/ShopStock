using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Services.Interfaces;

namespace ShopStock.Web.Controllers
{
    public class CatgoryController(IProuductService _ProuductService) : Controller
    {

        [Route("Category/{Slug}")]
        public async Task<IActionResult> Index(string Slug)
        {
            var Prouduct= await _ProuductService.GetProuductByCatgorySlugAsync(Slug);

            return View(Prouduct);
        }
    }
}
