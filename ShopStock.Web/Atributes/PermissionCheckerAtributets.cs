using Microsoft.AspNetCore.Mvc.Filters;
using ShopStock.Application.Extensions;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;

namespace ShopStock.Web.Atributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionCheckerAtributets(string PermissionName) : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var _permission=context.HttpContext.RequestServices.GetRequiredService<IPermissionService>();

                var UserId=context.HttpContext.User.GetUserId();

                bool UserHaveAction = await _permission.CheckUserPermissionAsync(UserId, PermissionName);

                if (!UserHaveAction)
                {

                    context.HttpContext.Response.StatusCode = 403;
                    context.HttpContext.Response.Redirect("/Admin/AccsesDenied");

                }



            }
            else
            {


                context.HttpContext.Response.StatusCode = 403;
                context.HttpContext.Response.Redirect("/Admin/AccsesDenied");
            }
        }
    }
}
