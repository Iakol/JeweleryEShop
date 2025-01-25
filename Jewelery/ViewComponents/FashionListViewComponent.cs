using Jewelery.Servise.FAQServise;
using Jewelery.Servise.FashionService;
using Jewelery.ViewModels.DTO.Review;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.ViewComponents
{
    public class FashionListViewComponent : ViewComponent
    {
        private readonly IFashionService _fashionService;

        public FashionListViewComponent(IFashionService fashionService)
        {
            _fashionService = fashionService;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            return View(_fashionService.getAllFashion());
        }
    }
}
