using Jewelery.Models.Review;
using Jewelery.ViewModels.DTO.Review;

namespace Jewelery.Servise.FAQServise
{
    public interface IFAQService
    {
        public void AddQuestion(FAQCSMDTO question); 
        public void UpdateQuestion(FAQCSMDTO question);
        public void DeleteQuestion(int id);

        public List<FAQVMDTO> getAllVMFAQ(int lang);
        public List<FAQCSMDTO> getAllCSMFAQ();
        public FAQCSMDTO getByIdCSMFAQ(int id);




    }
}
