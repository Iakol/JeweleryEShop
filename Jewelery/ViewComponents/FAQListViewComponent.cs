using Jewelery.Servise.FAQServise;
using Jewelery.ViewModels.DTO.Review;
using Microsoft.AspNetCore.Mvc;

namespace Jewelery.ViewComponents
{
    public class FAQListViewComponent : ViewComponent
    {
        private readonly IFAQService _faqService;

        public FAQListViewComponent(IFAQService faqService)
        {
            _faqService = faqService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int lang) 
        {
            return View(_faqService.getAllVMFAQ(lang));
        }
    }
}
