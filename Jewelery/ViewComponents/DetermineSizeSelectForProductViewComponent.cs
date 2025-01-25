using Jewelery.Servise.DetermineTheSizeService;
using Jewelery.Servise.FAQServise;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.ViewComponents
{
    public class DetermineSizeSelectForProductViewComponent : ViewComponent
    {

        public DetermineSizeSelectForProductViewComponent(IDetermineTheSizeService determineTheSizeService)
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(int index, int? selectedDetermoneSize)
        {
            if (selectedDetermoneSize != null)
            {
                ViewData["SelectedDetermineList"] = selectedDetermoneSize;
            }
            return View(index);
        }
    }
    public class DetermineSizeSelectForProductOptionsViewComponent : ViewComponent
    {
        private readonly IDetermineTheSizeService _determineTheSizeService;

        public DetermineSizeSelectForProductOptionsViewComponent(IDetermineTheSizeService determineTheSizeService)
        {
            _determineTheSizeService = determineTheSizeService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? selectedDetermoneSize)
        {
            if (selectedDetermoneSize != null) 
            {
                ViewData["SelectedDetermineList"] = selectedDetermoneSize;
            }
            return View(_determineTheSizeService.GetDetermineSizeSelectList());
        }
    }
}
