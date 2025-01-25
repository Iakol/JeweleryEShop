using Jewelery.data;
using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Jewelery.Servise.CategoryServise;
using Jewelery.Servise.ImageService;
using Jewelery.ViewModels.DTO.Category;
using Jewelery.ViewModels.DTO.SubCategory;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.Servise.SubCategoryServise
{
    public class SubCategoryServise : ISubCategoryServise
    {
        private readonly AppDBContext _db;
        private readonly IImageService _imageService;

        public SubCategoryServise(AppDBContext db, IImageService imageService) {
            _db = db;
            _imageService = imageService;
        }
        public void Add(SubCategoryCMSDTO entity)
        {

            if (entity.ImageFile != null)
            {
                entity.Image = _imageService.AddSubCategotyImage(entity);
            }
            SqlParameter Name_UKRPar = new SqlParameter("@Name_UKR", entity.Name_UKR);
            SqlParameter Name_ENGPar = new SqlParameter("@Name_ENG", entity.Name_ENG);
            SqlParameter Description_UKRPar = new SqlParameter("@Description_UKR", entity.Description_UKR);
            SqlParameter Description_ENGPar = new SqlParameter("@Description_ENG", entity.Description_ENG);
            SqlParameter Category_idPar = new SqlParameter("@Category_id", entity.Category_id);
            SqlParameter ImagePar = new SqlParameter("@Image", entity.Image);
            int max_value = 0;
            if (_db.SubCategories.Where(sc => sc.Category_id == entity.Category_id).Count() == 0)
            {

            }
            else
            {
                max_value = _db.SubCategories.Where(sc => sc.Category_id == entity.Category_id).Max(c => c.ViewOrder) + 1;

            }

            SqlParameter ViewOrderPar = new SqlParameter("@ViewOrder", entity.ViewOrder);




            _db.Database.ExecuteSqlRaw("EXECUTE AddSubCategory @Name_UKR, @Name_ENG, @Description_UKR, @Description_ENG,@Category_id, @Image, @ViewOrder",
                           Name_UKRPar, Name_ENGPar, Description_UKRPar, Description_ENGPar, Category_idPar, ImagePar, ViewOrderPar);
        }

        public void Delete(int id)
        {
            if (_db.SubCategories.FirstOrDefault(sc => sc.SubCategory_id == id) != null)
            {
                _imageService.DeleteImage(_db.SubCategories.Find(id).Image);
                SqlParameter SubCategory_IDPar = new SqlParameter("@SubCategory_ID", id);
                _db.Database.ExecuteSqlRaw("EXECUTE DeleteSubCategory @SubCategory_ID",
                    SubCategory_IDPar);

            }
            else {
                throw new J_NotFoundExeption();
            }
        }

        public List<CategoryDTO> GetByCategory(int lang, int Catagory_id)
        {
            if (_db.Categories.FirstOrDefault(c => c.Category_id == Catagory_id) != null)
            {
                SqlParameter Category_IdPar = new SqlParameter("@Category_Id", Catagory_id);
                SqlParameter LanguagePar = new SqlParameter("@Language", lang);
                return _db.Set<CategoryDTO>().FromSqlRaw("EXECUTE GetSubCategoryLocalizationTextByCategory @Category_Id, @Language",
                    Category_IdPar, LanguagePar).ToList();

            }
            else {
                throw new J_NotFoundExeption();
            }
        }

        public SubCategoryCMSDTO GetByIdCMS(int id)
        {
            return (SubCategoryCMSDTO)_db.Set<SubCategoryCMSDTO>().FromSqlRaw("EXECUTE GetSubCategotyCMS").ToList().FirstOrDefault(sc=>sc.SubCategory_id == id);
        }

        public void Update(SubCategoryCMSDTO entity)
        {
            if (_db.SubCategories.FirstOrDefault(sc => sc.SubCategory_id == entity.SubCategory_id) != null) 
            {
                if (entity.ImageFile != null)
                {
                    entity.Image = _imageService.AddSubCategotyImage(entity);
                }
                SqlParameter SubCategory_IdPar = new SqlParameter("@SubCategory_Id", entity.SubCategory_id);
                SqlParameter Name_UKRPar = new SqlParameter("@Name_UKR", entity.Name_UKR);
                SqlParameter Name_ENGPar = new SqlParameter("@Name_ENG", entity.Name_ENG);
                SqlParameter Description_UKRPar = new SqlParameter("@Description_UKR", entity.Description_UKR);
                SqlParameter Description_ENGPar = new SqlParameter("@Description_ENG", entity.Description_ENG);
                SqlParameter Category_IdPar = new SqlParameter("@Category_Id", entity.Category_id);
                SqlParameter ImagePar = new SqlParameter("@Image", entity.Image);
                SqlParameter ViewOrderPar = new SqlParameter("@ViewOrder", entity.ViewOrder);




                _db.Database.ExecuteSqlRaw("EXECUTE UpdateSubCategory @SubCategory_Id, @Name_UKR, @Name_ENG, @Description_UKR, @Description_ENG,@Category_Id, @Image,@ViewOrder",
                               SubCategory_IdPar, Name_UKRPar, Name_ENGPar, Description_UKRPar, Description_ENGPar, Category_IdPar, ImagePar , ViewOrderPar);

            }
            else { throw new J_NotFoundExeption(); }
        }
        public void UpdateOrder(List<CategoryPositionDTO> orderList)
        {
            foreach (var item in orderList)
            {
                var Cat = _db.SubCategories.Find(item.CategoryId);
                Cat.ViewOrder = item.Position;

            }
            _db.SaveChanges();
        }
    }
}
