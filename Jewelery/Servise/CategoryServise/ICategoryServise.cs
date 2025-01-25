using Jewelery.Models.Cart_Model;
using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Category;

namespace Jewelery.Servise.CategoryServise
{
    public interface ICategoryServise
    {
        List<CategoryDTO> GetAll(int lang);
        Task<List<CategoryVMDTO>> GetAllWithSubCategory(int lang);
        CategoryDTO GetById(int id, int lang);
        CategoryCMSDTO GetByIdCMS(int id);
        void Add(CategoryCMSDTO entity);
        void Update(CategoryCMSDTO entity);
        void Delete(int id);

        void UpdateOrder(List<CategoryPositionDTO> orderList);
    }
}
