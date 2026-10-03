using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Models.Catgories;
using ShopStock.Domain.ViewModels.Prouduct;
using ShopStock.Infra.Data.Context;

namespace ShopStock.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class ProuductController : Controller
    {


        IProuductService _ProuductService;
        ICatgoryService _catgoryservice;

        public ProuductController(IProuductService prouductService, ICatgoryService catgoryservice)
        {

            _ProuductService = prouductService;
            _catgoryservice = catgoryservice;
        }

        public async Task<IActionResult> Index(AdminFilterProuductViewModdel model)
        {

           var res=  await  _ProuductService.AdminFilterProuductGetAsync(model);

            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> CheckSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return Json(new
                {
                    exists = false
                });
            }

            var exists =
                await _ProuductService.IsExistSlugAsync(slug);

            return Json(new
            {
                exists
            });
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateProuductViewModel Create)
        {
            if (!ModelState.IsValid)
            {


                return View(Create);
            }
          

            await    _ProuductService.CreateProuductAsync(Create);

            return RedirectToAction("index");
        }


        #region Edit

        public async Task<IActionResult> Edit(int Id)
        {

            #region bindselect
            var prouduct =
                await _ProuductService.AdminProuductGetForEditAsync(Id);

            if (prouduct == null)
            {
                return NotFound();
            }


            var categories =
                await _catgoryservice.GetAllCatgoryForMegaAsync();

            if (categories == null)
            {
                return NotFound();
            }


            var selectedCategory =
                categories.FirstOrDefault(x => x.Id == prouduct.CatgoryId);

            if (selectedCategory == null)
            {
                return NotFound();
            }


            int mainCategoryId;
            int? subCategoryId = null;
            int? finalCategoryId = null;


            // سطح اول
            if (selectedCategory.ParentId == null)
            {
                mainCategoryId = selectedCategory.Id;
            }
            else
            {
                var parent =
                    categories.FirstOrDefault(
                        x => x.Id == selectedCategory.ParentId.Value
                    );

                if (parent == null)
                {
                    return NotFound();
                }


                // سطح دوم
                if (parent.ParentId == null)
                {
                    mainCategoryId = parent.Id;

                    subCategoryId =
                        selectedCategory.Id;
                }

                // سطح سوم
                else
                {
                    mainCategoryId =
                        parent.ParentId.Value;

                    subCategoryId =
                        parent.Id;

                    finalCategoryId =
                        selectedCategory.Id;
                }
            }


      


            // ==============================
            // Main
            // ==============================

            ViewBag.MainCategories =
                new SelectList(
                    categories.Where(x => x.ParentId == null),
                    "Id",
                    "Tittle",
                    mainCategoryId
                );


            // ==============================
            // Sub
            // ==============================

            ViewBag.SubCategories =
                new SelectList(
                    categories.Where(
                        x => x.ParentId == mainCategoryId
                    ),
                    "Id",
                    "Tittle",
                    subCategoryId
                );


            
            // Final
          

            if (subCategoryId.HasValue)
            {
                ViewBag.FinalCategories =
                    new SelectList(
                        categories.Where(
                            x => x.ParentId == subCategoryId.Value
                        ),
                        "Id",
                        "Tittle",
                        finalCategoryId
                    );
            }
            else
            {
                ViewBag.FinalCategories =
                    new SelectList(
                        Enumerable.Empty<Catgory>(),
                        "Id",
                        "Tittle"
                    );
            }
            #endregion



            return View(prouduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminEditProuductViewModel model)
        {

            if (!ModelState.IsValid) {

                return View(model);

            }

          await  _ProuductService.EditProuductAsync(model);

            return RedirectToAction("index");
        }

        #endregion
        [HttpPost]
        public async Task DeleteImage(int id) { 
        
      await  _ProuductService.DeleteImageGallery(id);
        
        }


    }
}
