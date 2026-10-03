using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Services.Interfaces;

namespace ShopStock.Web.Controllers
{
    public class ProuductController(IProuductService _service) : Controller
    {
        public async Task<PartialViewResult> ShowProuductDesc(int id)
        {

            var modalprouduct=await _service.GetProuductForShowModalByIdAsync(id);

            return PartialView(modalprouduct);
        }


        public async Task<IActionResult> ShowProuduct(int id) { 
        
        var prouduct=await _service.GetProuductPage(id);

            return View(prouduct);
        
        }

    }
}
