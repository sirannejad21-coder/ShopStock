using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;

namespace ShopStock.Web.Component
{

    public class TopProuductViewComponent(IProuductService _service) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _service.GetProuductForShowAsync();

            return View(data);
        }
    }
}