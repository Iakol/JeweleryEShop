using Jewelery.Servise.DetermineTheSizeService;
using Jewelery.Servise.FAQServise;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.ViewComponents
{
    public class DetermineSizeForProductViewPageViewComponent : ViewComponent
    {
        private readonly IDetermineTheSizeService _determineTheSizeService;
        public DetermineSizeForProductViewPageViewComponent(IDetermineTheSizeService determineTheSizeService)
        {
            _determineTheSizeService = determineTheSizeService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {            
            return View(_determineTheSizeService.GetDetermineTheSizePage(id));
        }
    }
    
}
