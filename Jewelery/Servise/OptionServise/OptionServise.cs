using Jewelery.data;
using Jewelery.Models.Cart_Model;
using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Product;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.Servise.ProductServise
{
    public class OptionServise : IOptionServise
    {
        private readonly AppDBContext _db;

        public OptionServise(AppDBContext db)
        {
            _db = db;
        }

        public void AddOption(Option entity)
        {
            _db.Options.Add(entity);
            _db.SaveChanges();
        }

        public List<Option> GetOptionByAtribute(int Atribute_id)
        {
            return _db.Options.Where(o => o.Atribute_id == Atribute_id).ToList();
        }

        public void RemoveOption(int Option_Id)
        {
            Option itemToDelete = _db.Options.Find(Option_Id);
            _db.Options.Remove(itemToDelete);
            _db.SaveChanges();
        }

        public void RemoveOptionByAtributeId(int Atribute_Id)
        {
            List<Option> itemsToDelete = _db.Options.Where(o => o.Atribute_id == Atribute_Id).ToList();
            _db.Options.RemoveRange(itemsToDelete);
            _db.SaveChanges();
        }

        public void UpdateOprion(Option entity)
        {
            Option optToupd = _db.Options.FirstOrDefault(o => o.Option_id == entity.Option_id);
            optToupd.PriceAdjustment = entity.PriceAdjustment;
            optToupd.Size = entity.Size;
            _db.SaveChanges();
        }
    }
}
