using Jewelery.data;
using Jewelery.Infrastructure;
using Jewelery.Migrations;
using Jewelery.Models.Review;
using Jewelery.ViewModels.DTO.Review;
using Microsoft.Data.SqlClient;

namespace Jewelery.Servise.FAQServise
{
    public class FAQService : IFAQService
    {
        private readonly AppDBContext _db;

        public FAQService(AppDBContext db)
        {
            _db = db;
        }

        public void AddQuestion(FAQCSMDTO question)
        {
            LocalizationModel localeQuestion = new LocalizationModel
            {
                Type_Variable = 11,
                UKR = question.Question_UKR,
                ENG = question.Question_ENG
            };
            LocalizationModel localeAnswer = new LocalizationModel
            {
                Type_Variable = 12,
                UKR = question.Answer_UKR,
                ENG = question.Answer_ENG
            };

            _db.Localizations.Add(localeQuestion);
            _db.Localizations.Add(localeAnswer);

            _db.SaveChanges();

            FAQ faqToAdd = new FAQ
            {
                Question = localeQuestion.Value_Id,
                Answer = localeAnswer.Value_Id
            };

            _db.FAQs.Add(faqToAdd);
            _db.SaveChanges();

        }

        public void DeleteQuestion(int id)
        {
            FAQ FaqToDelete = _db.FAQs.Find(id);
            _db.Localizations.Remove(_db.Localizations.Find(FaqToDelete.Question));
            _db.Localizations.Remove(_db.Localizations.Find(FaqToDelete.Answer));
            _db.FAQs.Remove(FaqToDelete);
            _db.SaveChanges();
        }

        public List<FAQCSMDTO> getAllCSMFAQ()
        {
            List<FAQCSMDTO> list = _db.FAQs.Select(f => new FAQCSMDTO
            {
                Id = f.Id,
                Question_UKR = _db.Localizations.FirstOrDefault( l => l.Value_Id == f.Question).UKR,
                Question_ENG = _db.Localizations.FirstOrDefault(l => l.Value_Id == f.Question).ENG,
                Answer_UKR = _db.Localizations.FirstOrDefault(l => l.Value_Id == f.Answer).UKR,
                Answer_ENG = _db.Localizations.FirstOrDefault(l => l.Value_Id == f.Answer).ENG,
            }).ToList();

            return list;
        }

        public List<FAQVMDTO> getAllVMFAQ(int lang)
        {
            List<FAQVMDTO> list = new List<FAQVMDTO>();

            switch (lang) 
            {
                case 1:
                    list = _db.FAQs.Select(f => new FAQVMDTO
                    {
                        Id = f.Id,
                        Question = _db.Localizations.FirstOrDefault(l => l.Value_Id == f.Question).UKR,
                        Answer = _db.Localizations.FirstOrDefault(l => l.Value_Id == f.Answer).UKR,
                    }).ToList();
                    return list;
                case 2:
                    list = _db.FAQs.Select(f => new FAQVMDTO
                    {
                        Id = f.Id,
                        Question = _db.Localizations.FirstOrDefault(l => l.Value_Id == f.Question).ENG,
                        Answer = _db.Localizations.FirstOrDefault(l => l.Value_Id == f.Answer).ENG
                    }).ToList();
                    return list;
            }      

            return list;

        }

        public FAQCSMDTO getByIdCSMFAQ(int id)
        {
            return getAllCSMFAQ().FirstOrDefault(f => f.Id == id);
        }

        public void UpdateQuestion(FAQCSMDTO question)
        {
            FAQ fagToUpdate = _db.FAQs.FirstOrDefault(f => f.Id == question.Id);

            LocalizationModel localeQuestion = _db.Localizations.FirstOrDefault(l => l.Value_Id == fagToUpdate.Question);
            localeQuestion.ENG = question.Question_ENG;
            localeQuestion.UKR = question.Question_UKR;
            LocalizationModel localeAnswer = _db.Localizations.FirstOrDefault(l => l.Value_Id == fagToUpdate.Answer);
            localeAnswer.ENG = question.Answer_ENG;
            localeAnswer.UKR = question.Answer_UKR;

            _db.Localizations.Update(localeQuestion);
            _db.Localizations.Update(localeAnswer);

            _db.SaveChanges();


        }
    }
}
