using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Mapper;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.ViewModels.Catgory;
using ShopStock.Domain.ViewModels.User;

namespace ShopStock.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CatgoryController(ICatgoryService _catgoryService) : Controller
    {
        #region Index

        public async Task<IActionResult> Index(
            AdminFilterCatgoryViewModel filter)
        {
            var result = await _catgoryService.FilterAsync(filter);

            return View(result);
        }

        #endregion


        #region SubCategory

        public async Task<IActionResult> SubCatgory(int id)
        {
            var filter = new AdminFilterCatgoryViewModel
            {
                ParentId = id
            };

            var result = await _catgoryService.FilterAsync(filter);

            return View("Index", result);
        }

        #endregion


        #region Create

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create(int? id)
        {
            var model = new AdminCreateCatgoryViewModel
            {
                ParentId = id
            };

            // اگر قرار است زیرگروه ایجاد شود
            if (id.HasValue)
            {
                var parentCategory =
                    await _catgoryService.GetCatgoryByIdAsync(id.Value);

                if (parentCategory == null)
                {
                    return NotFound();
                }

                model.CatgoryParentTittle =
                    parentCategory.Tittle;
            }

            return View(model);
        }


        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            AdminCreateCatgoryViewModel model)
        {
            // اعتبارسنجی اولیه
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            // بررسی تکراری نبودن Slug
            if (await _catgoryService.IsExistSlugAsync(model.Slug))
            {
                ModelState.AddModelError(
                    nameof(model.Slug),
                    "این آدرس قبلاً استفاده شده است.");

                return View(model);
            }


            // ایجاد گروه
            await _catgoryService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AdminEditCatgoryViewModel model = new AdminEditCatgoryViewModel();

            var data = await _catgoryService.GetCatgoryByIdAsync(id.Value);

            if (data.ParentId != null)
            {
                var parentCategory =
                     await _catgoryService.GetCatgoryByIdAsync(data.ParentId.Value);



                model.CatgoryParentTittle =
                    parentCategory.Tittle;
            }

            model.Slug = data.Slug;
            model.Tittle = data.Tittle;
            model.CatgoryId = data.Id;
            model.ParentId = data.ParentId;
            model.ImageName = data.ImageName;

            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminEditCatgoryViewModel model)
        {

        



            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // بررسی تکراری نبودن Slug
            if (await _catgoryService.IsExistSlugAsync(model.Slug,model.CatgoryId))
            {
                ModelState.AddModelError(
                    nameof(model.Slug),
                    "این آدرس قبلاً استفاده شده است.");

                return View(model);
            }

            await _catgoryService.EditByAdminAsync(model);

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Delete
        public async Task<IActionResult> Delete(int id)
        {

            if (id == null)
            {

                return NotFound();
            }

            var catgory = await _catgoryService.GetCatgoryByIdAsync(id);

            if (catgory == null) { 
            
            return NotFound();
            }


            return View(catgory);

        }


          [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


      await  _catgoryService.DeleteAsync(id);    


            return RedirectToAction("index");

        }
        #endregion


        #region json

        public async Task<JsonResult> GetCatgories(int? parentid)
        {

            var catgory= await  _catgoryService.GetCatgories(parentid);    

            return Json(catgory);



        }

        #endregion
    }

}
