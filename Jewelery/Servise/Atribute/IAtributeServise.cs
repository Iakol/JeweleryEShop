using Jewelery.Models.Cart_Model;
using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Atribute;
using Jewelery.ViewModels.DTO.Product;

namespace Jewelery.Servise.ProductServise
{
    public interface IAtributeServise
    {
        public void AddAtribute(AtributeCMSDTO entiry);
        public void UpdateAtribute(AtributeCMSDTO entiry);
        public List<AtributeDTO> GetAtributeByProduct(int Product_id, int lang);
        public List<AtributeCMSDTO> GetAtributeByProductCMS(int Product_id);

        public void DeleteAtribute(int Atribute_id);

    }
}
