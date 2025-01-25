using Jewelery.Models.Cart_Model;
using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Category;
using Jewelery.ViewModels.DTO.SubCategory;

namespace Jewelery.Servise.CategoryServise
{
    public interface ISubCategoryServise
    {
        List<CategoryDTO> GetByCategory(int lang, int Catagory_id);
        SubCategoryCMSDTO GetByIdCMS(int id);
        void Add(SubCategoryCMSDTO entity);
        void Update(SubCategoryCMSDTO entity);
        void Delete(int id);
        public void UpdateOrder(List<CategoryPositionDTO> orderList);
    }
}
