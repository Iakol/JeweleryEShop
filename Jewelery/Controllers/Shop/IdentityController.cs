using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Jewelery.Servise.IdentityService;
using Jewelery.ViewModels.DTO.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jewelery.Controllers.Shop
{
    public class IdentityController : Controller
    {
        private readonly IIdentityServise _identityServise;

        public IdentityController(IIdentityServise identityServise)
        {
            _identityServise = identityServise;
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LogInDTOVM cred)
        {
            var result = await _identityServise.Login(cred);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(LoginRedirect));
            }
            else 
            {
                ModelState.AddModelError(string.Empty, "Login or password is wrong");
                return View(cred);
            
            }
        }

        public IActionResult LoginRedirect() 
        {
            if (!User.Identity.IsAuthenticated)
            {
                throw new J_BadRequestExeption("User Not Auth");
            }
            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (User.IsInRole("SuperAdmin") || User.IsInRole("Admin"))
            {
                return RedirectToAction("CMSMain", "CMS");
            }
            else
            {
                throw new J_BadRequestExeption("Unknown Role");
            }
        }


        [Route("Registration")]
        public IActionResult Register() 
        {
            return View();
        }

        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTOVM cred) 
        {
            var result = await _identityServise.RegisterUser(cred);

            if (result) {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Bad Request");
            return RedirectToAction(nameof(Register));
        }

        public async Task<IActionResult> LogOut() 
        {
            await _identityServise.Logout();
            return  RedirectToAction("Index", "Home");
        }

    }
}
