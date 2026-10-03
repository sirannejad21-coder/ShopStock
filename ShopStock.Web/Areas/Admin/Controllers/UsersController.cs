using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Generator;
using ShopStock.Application.Mapper;
using ShopStock.Application.Security;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Application.Statics;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Users;
using ShopStock.Domain.ViewModels.Common;
using ShopStock.Domain.ViewModels.User;
using ShopStock.Infra.Data.Context;
using ShopStock.Web.Atributes;

namespace ShopStock.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [PermissionCheckerAtributets(PermissionNames.ManagePanel)]
    public class UsersController : Controller
    {
        #region constractor
      
        IUserService _Service;
        IRoleService _RoleService;

        public UsersController( IUserService service, IRoleService roleService)
        {
          
            _Service = service;
            _RoleService = roleService;
        }
        #endregion

        #region index
        [PermissionCheckerAtributets(PermissionNames.ManageUser)]
        public async Task<IActionResult> Index(AdminFilterUserViewModel Filter)
        {
            return View(await _Service.adminFilterUserViewModel(Filter));
        }
        #endregion

        #region Create Admin

        [HttpGet]
       [PermissionCheckerAtributets(PermissionNames.AddUser)]
        public async Task<IActionResult> Create()
        {

       
            return View(new AdminCreateViewModel
            {
               
                Roles = await _RoleService.GetRolesAsync(),
                IsActive = true,
                Avatar = "nophoto.jpg"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerAtributets(PermissionNames.AddUser)]
        public async Task<IActionResult> Create(AdminCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles =await _RoleService.GetRolesAsync();
                return View(model);
            }

            await _Service.CreatCreatAdminUserAsyncAdminUser(model);
          

                return RedirectToAction(nameof(Index));
            }

        #endregion

        #region Delete Admin

        [HttpGet]
        [Route("Delete/{id}")]

        [PermissionCheckerAtributets(PermissionNames.DeleteUser)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _Service.GetUserForDelete(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost]

       [PermissionCheckerAtributets(PermissionNames.DeleteUser)]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _Service.DeleteByAdmin(id);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Edit Admin
        [PermissionCheckerAtributets(PermissionNames.EditUser)]
        public async Task<IActionResult> Edit(int id)
        {
     var user=   await    _Service.GetUserForEdit(id);

         var mapper=   UserMapper.MappertoAdminEdit(user);

            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Roles = await _RoleService.GetRolesAsync();
            return View(mapper);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionCheckerAtributets(PermissionNames.EditUser)]
        public async Task<IActionResult> Edit(AdminEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // اینجا بعداً Roles را دوباره پر می‌کنیم
                return View(model);
            }

            await  _Service.EditByAdmin(model);

            return RedirectToAction(nameof(Index));
        }

        #endregion




    }
}

