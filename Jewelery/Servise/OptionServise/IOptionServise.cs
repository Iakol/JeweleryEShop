using Jewelery.Models.Cart_Model;
using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Product;

namespace Jewelery.Servise.ProductServise
{
    public interface IOptionServise
    {
        public void AddOption(Option entity);
        public void UpdateOprion(Option entity);

        public List<Option> GetOptionByAtribute(int Atribute_id);
        public void RemoveOption(int Option_Id);
        public void RemoveOptionByAtributeId(int Atribute_Id);
    }
}
