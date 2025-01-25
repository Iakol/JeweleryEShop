using Jewelery.Servise.DetermineTheSizeService;
using Jewelery.ViewModels.DTO.DetermineTheSizeEditor;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.Controllers.CMS
{
    public class DetermineTheSizeController : Controller
    {
        private readonly IDetermineTheSizeService _determineTheSizeService;

        public DetermineTheSizeController(IDetermineTheSizeService determineTheSizeService) 
        {
            _determineTheSizeService = determineTheSizeService;
        }
        public IActionResult DetermineTheSizeCMS()
        {
            List < DetermineTheSizePageDTO > list = _determineTheSizeService.GetDetermineTheSizePageList();
            return View(list);
        }

        public IActionResult DeleteDetermineTheSize(int id)
        {
            _determineTheSizeService.DeleteDetermineTheSizePage(id);
            return RedirectToAction(nameof(DetermineTheSizeCMS));
        }
        public IActionResult DetermineTheSizeEditor(int? id) 
        {
            DetermineTheSizePageDTO page = new DetermineTheSizePageDTO();
            
            if (id != null)
            {
                page = _determineTheSizeService.GetDetermineTheSizePage(id.Value);
            }
            
            return View(page);
        }

        [HttpPost]
        public IActionResult CreateDetermineTheSize(DetermineTheSizePageObjectFormDTO obj) 
        {
            _determineTheSizeService.SetUpDetermineTheSize(obj);
            return Ok();
        }


        public IActionResult getDetermibeSizeListComponent( int index)
        {
            return ViewComponent("DetermineSizeSelectForProduct", new { index = index });
        }
    }
}
