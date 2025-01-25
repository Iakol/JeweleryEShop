using Jewelery.Servise.CartServise;
using Jewelery.Servise.CategoryServise;
using Jewelery.Servise.IdentityService;
using Jewelery.Servise.SessionService;
using Jewelery.ViewModels.DTO.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.ViewComponents
{
    public class CreateOrderCartViewComponent : ViewComponent
    {
        private readonly ICartServise _cartServise;
        private readonly ISessionService _sessionService;
        private readonly IIdentityServise _identityServise;

       public CreateOrderCartViewComponent(ICartServise cartServise, ISessionService sessionService, IIdentityServise identityServise)
        {
            _cartServise = cartServise;
            _sessionService = sessionService;
            _identityServise = identityServise;
        }

        public async Task<IViewComponentResult> InvokeAsync(int lang, HttpContext context)
        {
            CartDTOVM list = new CartDTOVM();
            if (User.Identity.IsAuthenticated)
            {
                list = _cartServise.GetCartByUserDTOVM(_identityServise.GetUser(context), lang);

            }
            else
            {
                list = _sessionService.GetCartByUserDTOVM(lang, context);
            }
            return View(list);
        }     
    }
}
