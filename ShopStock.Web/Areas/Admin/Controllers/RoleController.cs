using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Mapper;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Roles;
using ShopStock.Domain.ViewModels.Role;

namespace ShopStock.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RoleController(IRoleService _RoleService,IPermissionService _permission) : Controller
    {
        #region index
        public async Task<IActionResult> Index()
        {
            var role = await _RoleService.GetRolesAsync();


            return View(UserMapper.mappertoadmincreatrole(role.ToList()));
        }
        #endregion
        #region creatrole
        public async Task<IActionResult> Create()
        {

            AdminCreateRoleViewModel admin = new AdminCreateRoleViewModel()
            {

                Permissions = await _permission.GetPermissionsAsync()
            };

            return View(admin);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create(AdminCreateRoleViewModel model, List<int> PermissionSelectedId)
        {
            if (!ModelState.IsValid) {

                return View(model);
            }

            await _RoleService.AdminCreateRolesAsync(model, PermissionSelectedId);


            return RedirectToAction("index");
        }

    
        #endregion


    #region edit

    public async Task<IActionResult> Edit(int Id)
        {
            var Role= await _RoleService.GetRoleByIdAsync(Id);

            if (Role == null)
            {
                return NotFound();
            }

            ViewBag.permission= await _permission.GetPermissionsAsync();

            return View(Role);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Role role,List<int> PermissionSelectedId)
        {

         await   _RoleService.AdminEditRole(role, PermissionSelectedId);
            return RedirectToAction("index");  

        }


        #endregion


        public async Task<IActionResult> Delete(int Id) {
        
        var role= await _RoleService.GetRoleByIdAsync(Id);


            if (role == null)
            {

                return NotFound(role);  
            }

            var rolemodel = new AdminDeleteRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.RoleName

            };



            return View(rolemodel);
        
        
        }
        [HttpPost]
        public async Task<IActionResult> Delete(AdminDeleteRoleViewModel model)
        {

          await  _RoleService.DeleteRole(model.Id);

            return RedirectToAction("index");


        }
        
        }
}