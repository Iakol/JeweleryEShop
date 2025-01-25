using Jewelery.data;
using Jewelery.Models.Cart_Model;
using Jewelery.Models.Product_model;
using Jewelery.Servise.ImageService;
using Jewelery.Servise.ProductServise;
using Jewelery.ViewModels.DTO.Category;
using Jewelery.ViewModels.DTO.SubCategory;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.Servise.CategoryServise
{
    public class CategoryServise : ICategoryServise
    {
        private readonly AppDBContext _db;
        private readonly IImageService _imageService;


        public CategoryServise(AppDBContext db, IImageService imageService)
        {
            _db = db;
            _imageService = imageService;
        }

        public void Add(CategoryCMSDTO entity)
        {
            if(entity.ImageFile != null)
            {
                entity.Image = _imageService.AddCategotyImage(entity);
            }
            SqlParameter Name_UKRPar = new SqlParameter("@Name_UKR", entity.Name_UKR);
            SqlParameter Name_ENGPar = new SqlParameter("@Name_ENG", entity.Name_ENG);
            SqlParameter Description_UKRPar = new SqlParameter("@Description_UKR", entity.Description_UKR);
            SqlParameter Description_ENGPar = new SqlParameter("@Description_ENG", entity.Description_ENG);
            SqlParameter ImagePar = new SqlParameter("@Image", entity.Image);
            int max_value = 0;
            if (_db.Categories.Count() == 0) 
            {
                
            }
            else
            {
                max_value = _db.Categories.Max(c => c.ViewOrder) + 1;

            }
            SqlParameter ViewOrderPar = new SqlParameter("@ViewOrder", max_value);




            _db.Database.ExecuteSqlRaw("EXECUTE AddCategory @Name_UKR, @Name_ENG, @Description_UKR, @Description_ENG, @Image, @ViewOrder",
                            Name_UKRPar, Name_ENGPar, Description_UKRPar, Description_ENGPar, ImagePar, ViewOrderPar);

        }

        public void Delete(int id)
        {
            Category itemtodelete = _db.Categories.Include(sc => sc.SubCategories).FirstOrDefault(c => c.Category_id == id);
            if (itemtodelete != null)
            {
                _imageService.DeleteImage(itemtodelete.Image);
                foreach (var subitem in itemtodelete.SubCategories) {
                    _imageService.DeleteImage(subitem.Image);

                }
                SqlParameter Category_ID = new SqlParameter("@Category_ID", id);
                _db.Database.ExecuteSqlRaw("EXECUTE DeleteCategory @Category_ID", Category_ID);
            }
            else
            {
                throw new Exception();
            }
        }

        public List<CategoryDTO> GetAll(int lang)
        {
            SqlParameter Language = new SqlParameter("@Language", lang);
            return _db.Set<CategoryDTO>().FromSqlRaw("EXECUTE GetCategoryLocalizationText @Language", Language).ToList(); 
        }

        public async Task<List<CategoryVMDTO>> GetAllWithSubCategory( int lang)
        {
            SqlParameter Language = new SqlParameter("@Language", lang);
            List<CategoryVMDTO> CategoryList = await _db.Set<CategoryVMDTO>().FromSqlRaw("EXECUTE GetCategoryLocalizationText @Language", Language).ToListAsync();
            foreach(var item in CategoryList)
            {
                SqlParameter Category_id = new SqlParameter("@Category_id", item.Category_id);
                 item.SubCategories = await _db.Set<SubCategoryVMDTO>().FromSqlRaw("EXECUTE GetSubCategoryForCategoryList @Language, @Category_id", Language, Category_id).ToListAsync();

            }
            return CategoryList;
        }

        public CategoryDTO GetById(int id, int lang)
        {
            SqlParameter Language = new SqlParameter("@Language", lang);
            return (CategoryDTO)_db.Set<CategoryDTO>().FromSqlRaw("EXECUTE GetCategoryLocalizationText @Language", Language).AsEnumerable().FirstOrDefault(c => c.Category_id == id);
        }

        public CategoryCMSDTO GetByIdCMS(int id)
        {
            var items = _db.Set<CategoryCMSDTO>().FromSqlRaw("EXECUTE GetCategotyCMS").ToList();
            var item = items.FirstOrDefault(c => c.Category_id == id);
            return item;
        }

        public void Update(CategoryCMSDTO entity)
        {
            Category itemtoUpdate = _db.Categories.FirstOrDefault(c => c.Category_id == entity.Category_id);
            if (itemtoUpdate != null)
            {
                if (entity.ImageFile != null)
                {
                    entity.Image = _imageService.AddCategotyImage(entity);
                }
                SqlParameter Category_IdPar = new SqlParameter("@Category_Id", entity.Category_id);
                SqlParameter Name_UKRPar = new SqlParameter("@Name_UKR", entity.Name_UKR);
                SqlParameter Name_ENGPar = new SqlParameter("@Name_ENG", entity.Name_ENG);
                SqlParameter Description_UKRPar = new SqlParameter("@Description_UKR", entity.Description_UKR);
                SqlParameter Description_ENGPar = new SqlParameter("@Description_ENG", entity.Description_ENG);
                SqlParameter ImagePar = new SqlParameter("@Image", entity.Image);
                SqlParameter ViewOrderPar = new SqlParameter("@ViewOrder", entity.ViewOrder);




                _db.Database.ExecuteSqlRaw("EXECUTE UpdateCategory @Category_Id, @Name_UKR, @Name_ENG, @Description_UKR, @Description_ENG, @Image, @ViewOrder ",
                               Category_IdPar, Name_UKRPar, Name_ENGPar, Description_UKRPar, Description_ENGPar, ImagePar, ViewOrderPar);
            }
            else
            {
                throw new Exception();
            }

        }

        public void UpdateOrder(List<CategoryPositionDTO> orderList)
        {
            foreach (var item in orderList) 
            {
                var Cat = _db.Categories.Find(item.CategoryId);
                Cat.ViewOrder = item.Position;

            }
            _db.SaveChanges();
        }
    }
}
