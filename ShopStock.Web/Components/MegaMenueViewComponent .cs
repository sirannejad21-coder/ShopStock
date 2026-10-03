using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;

namespace ShopStock.Web.Component
{

    public class MegaMenueViewComponent(ICatgoryService _service) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _service.GetAllCatgoryForMegaAsync();

            return View(data);
        }
    }
}