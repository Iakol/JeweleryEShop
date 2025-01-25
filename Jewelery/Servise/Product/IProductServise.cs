using Jewelery.Models.Cart_Model;
using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Product;

namespace Jewelery.Servise.ProductServise
{
    public interface IProductServise
    {
        List<ProductDTOVMPage> GetAll(int lang);
        ProductDTOVMPage GetById(int id, int lang);

        IEnumerable<ProductCMSDTO> GetAllCMS();
        ProductCMSDTO GetByIdCMS(int id);
        void Add(ProductCMSDTO entity);
        void Update(ProductCMSDTO entity);
        void Delete(int id);
    }
}
