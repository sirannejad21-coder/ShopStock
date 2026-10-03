using Microsoft.AspNetCore.Mvc;
using ShopStock.Domain.Contracts;

namespace ShopStock.Web.Component
{
   
    public class UserPanelViewComponent : ViewComponent
    {
        IUserRepository _userRepository;

        public UserPanelViewComponent(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userRepository.GetUserByEmailOrUserNameAsync(User.Identity.Name);
            return View(user);
        }
    }
}