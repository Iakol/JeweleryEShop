using Jewelery.Models.Review;
using Jewelery.Servise.FAQServise;
using Jewelery.Servise.FashionService;
using Jewelery.ViewModels.DTO.Review;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.Controllers.CMS
{
    public class ReviewController : Controller
    {
        private readonly IFAQService _faqService;
        private readonly IFashionService _fashionService;

        public ReviewController(IFAQService faqService, IFashionService fashionService)
        {
            _faqService = faqService;
            _fashionService = fashionService;
        }

        public IActionResult Fashion()
        {
            return View(_fashionService.getAllFashion());
        }

        [HttpPost]
        public IActionResult AddFashion(FashionCSMDTO fashion) 
        {
            _fashionService.AddFashion(fashion);
            return RedirectToAction(nameof(Fashion));
        }

        public IActionResult DeleteFashion(int id) 
        {
            _fashionService.RemoveFashion(id);
            return RedirectToAction(nameof(Fashion));
        }

        public IActionResult FAQ()
        {
            return View(_faqService.getAllCSMFAQ());
        }

        [HttpPost]
        public IActionResult AddFAQ(FAQCSMDTO FAQ)
        {
            _faqService.AddQuestion(FAQ);
            return RedirectToAction(nameof(FAQ));
        }

        [HttpPost]
        public IActionResult EditFAQ(FAQCSMDTO FAQ)
        {
            _faqService.UpdateQuestion(FAQ);
            return RedirectToAction(nameof(FAQ));
        }

        public IActionResult GetFAQById(int id)
        {
            return Json(_faqService.getByIdCSMFAQ(id));
        }

        public IActionResult DeleteFAQ(int id)
        {
            _faqService.DeleteQuestion(id);
            return RedirectToAction(nameof(FAQ));
        }

    }
}
