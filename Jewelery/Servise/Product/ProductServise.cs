using Jewelery.data;
using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Jewelery.Models.Cart_Model;
using Jewelery.Models.Product_model;
using Jewelery.Servise.ImageService;
using Jewelery.ViewModels.DTO.Atribute;
using Jewelery.ViewModels.DTO.Product;
using Jewelery.ViewModels.DTO.Product_images;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace Jewelery.Servise.ProductServise
{
    public class ProductServise : IProductServise
    {
        private readonly AppDBContext _db;
        private readonly IAtributeServise _atributeServise;
        private readonly IImageService _imageService;


        public ProductServise(AppDBContext db, IAtributeServise atributeServise, IImageService imageService)
        {
            _db = db;
            _atributeServise = atributeServise;
            _imageService = imageService;
        }

        public void Add(ProductCMSDTO entity)
        {
            SqlParameter Name_UKR = new SqlParameter("@Name_UKR", entity.Name_UKR);
            SqlParameter Name_ENG = new SqlParameter("@Name_ENG", entity.Name_ENG);
            SqlParameter Description_UKR = new SqlParameter("@Description_UKR", entity.Description_UKR);
            SqlParameter Description_ENG = new SqlParameter("@Description_ENG", entity.@Description_ENG);
            SqlParameter Price = new SqlParameter("@Price", entity.Price);
            SqlParameter Category_id = new SqlParameter("@Category_id", entity.Category_id);
            SqlParameter Articul = new SqlParameter("@Articul", entity.Articul);
            SqlParameter SubCategory_id = new SqlParameter("@SubCategory_id", DBNull.Value);
            if (entity.SubCategory_id != null)
            {
                SubCategory_id = new SqlParameter("@SubCategory_id", entity.SubCategory_id);

            }
            
            SqlParameter isExist = new SqlParameter("@isExist", entity.isExist);
            SqlParameter isDisplay = new SqlParameter("@isDisplay", entity.isDisplay);
            SqlParameter isPromotion = new SqlParameter("@isPromotion", entity.isPromotion);
            SqlParameter Promotion_Price = new SqlParameter("@Promotion_Price", DBNull.Value);
            if (entity.Promotion_Price != null)
            {
                Promotion_Price = new SqlParameter("@Promotion_Price", entity.Promotion_Price);

            }


            SqlParameter CreateProductId = new SqlParameter
            {
                ParameterName = "@CreateProductId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            _db.Database.ExecuteSqlRaw("EXECUTE AddProduct @Name_UKR,@Name_ENG,@Description_UKR,@Description_ENG,@Price,@Category_id,@Articul,@SubCategory_id,@isExist,@isDisplay, @isPromotion, @Promotion_Price, @CreateProductId OUTPUT",
                Name_UKR, Name_ENG, Description_UKR, Description_ENG, Price, Category_id, Articul, SubCategory_id, isExist, isDisplay, isPromotion, Promotion_Price,  CreateProductId);

            entity.Product_id = (int)CreateProductId.Value;

            var product = _imageService.AddImageToProduct(entity);

            if(product.Images != null) 
            {
                foreach (var item in product.Images)
                {

                    _db.Product_images.Add(new Product_images
                    {
                        Image_id = item.Image_id,
                        Product_id = entity.Product_id,
                        Url = item.Url,
                        Alt_text = item.Alt_text

                    });

                }

                _db.SaveChanges();
            }

            
            if (entity.Attributes != null) 
            {
                foreach (var item in entity.Attributes)
                {
                    item.Product_id = entity.Product_id;
                    _atributeServise.AddAtribute(item);
                }
            }              


        }

        public void Delete(int id)
        {
            if (_db.Products.FirstOrDefault(p => p.Product_id == id) != null)
            {
                var photoList = _db.Product_images.Where(i => i.Product_id == id).ToList();                
                SqlParameter Product_id = new SqlParameter("@Product_Id", id);
                _db.Database.ExecuteSqlRaw("EXECUTE DeleteProduct @Product_Id",
                    Product_id);
                _imageService.DeleteImageFromProduct(photoList);


            }
            else { throw new J_NotFoundExeption(); }
        }

        public List<ProductDTOVMPage> GetAll(int lang)
        {
            
            SqlParameter Language = new SqlParameter("@Language", lang);
            List<ProductDTOVMPage> listItem = _db.Set<ProductDTOVMPage>().FromSqlRaw("EXECUTE GetAllProduct @Language",
                Language).ToList();

            foreach (ProductDTOVMPage item in listItem) 
            {
                item.Attributes = _atributeServise.GetAtributeByProduct(item.Product_id, lang);
            }

            foreach (ProductDTOVMPage item in listItem)
            {
                item.Images = _db.Product_images.Where(i => i.Product_id == item.Product_id).Select(i => new Product_imagesDTO 
                {
                    Image_id = i.Image_id,
                    Product_id  = i.Product_id,
                    Url = i.Url,
                    ImageFile = null,
                    Alt_text = i.Alt_text    }                
                ).ToList();
            }

            return listItem;
        }

        public IEnumerable<ProductCMSDTO> GetAllCMS()
        {
            List<ProductCMSDTO> listItem = _db.Set<ProductCMSDTO>().FromSqlRaw("EXECUTE GetAllProductCMS").ToList();

            foreach (ProductCMSDTO item in listItem)
            {
                item.Attributes = _atributeServise.GetAtributeByProductCMS(item.Product_id);
                item.Images = _db.Product_images.Where(i => i.Product_id == item.Product_id).Select(i => new Product_imagesDTO {
                    Image_id = i.Image_id,
                    Product_id = i.Product_id,
                    Url = i.Url,
                    ImageFile = null,
                    Alt_text = i.Alt_text  
                 }).ToList();
            }

            return listItem;
        }

        public ProductDTOVMPage GetById(int id, int lang)
        {
            if (_db.Products.FirstOrDefault(p => p.Product_id == id) != null)
            {
                SqlParameter Language = new SqlParameter("@Language", lang);
                SqlParameter Product_id = new SqlParameter("@Product_id", id);

                ProductDTOVMPage item = (ProductDTOVMPage)_db.Set<ProductDTOVMPage>().FromSqlRaw("EXECUTE GetByIDProduct @Language, @Product_id",
                    Language, Product_id).AsEnumerable().First();

                item.Attributes = _atributeServise.GetAtributeByProduct(item.Product_id, lang);
                item.Images = _db.Product_images.Where(i => i.Product_id == item.Product_id).Select(i => new Product_imagesDTO
                {
                    Image_id = i.Image_id,
                    Product_id = i.Product_id,
                    Url = i.Url,
                    ImageFile = null,
                    Alt_text = i.Alt_text
                }).ToList();
                return item;
            }
            else {
                throw new J_NotFoundExeption();
            }        

        }

        public ProductCMSDTO GetByIdCMS(int id)
        {
            if (_db.Products.FirstOrDefault(p => p.Product_id == id) != null)
            {
                SqlParameter Product_id = new SqlParameter("@Product_id", id);

                var item = (ProductCMSDTO)_db.Set<ProductCMSDTO>().FromSqlRaw("EXECUTE GetByIDProductCMS @Product_id",
                    Product_id).AsEnumerable().First();
                item.Attributes =  _atributeServise.GetAtributeByProductCMS(item.Product_id);
                item.Images = _db.Product_images.Where(i => i.Product_id == item.Product_id)
                    .Select(i => new Product_imagesDTO {
                        Image_id = i.Image_id,
                        Product_id = i.Product_id,
                        Url = i.Url,
                        Alt_text = i.Alt_text
                    }).ToList();
                return item;
            }
            else
            {
                throw new J_NotFoundExeption();
            }
        }

        public void Update(ProductCMSDTO entity)
        {
            if (_db.Products.FirstOrDefault(p => p.Product_id == entity.Product_id) != null)
            {
                SqlParameter Product_id = new SqlParameter("@Product_id", entity.Product_id);
                SqlParameter Name_UKR = new SqlParameter("@Name_UKR", entity.Name_UKR);
                SqlParameter Name_ENG = new SqlParameter("@Name_ENG", entity.Name_ENG);
                SqlParameter Description_UKR = new SqlParameter("@Description_UKR", entity.Description_UKR);
                SqlParameter Description_ENG = new SqlParameter("@Description_ENG", entity.@Description_ENG);
                SqlParameter Price = new SqlParameter("@Price", entity.Price);
                SqlParameter Category_id = new SqlParameter("@Category_id", entity.Category_id);
                SqlParameter Articul = new SqlParameter("@Articul", entity.Articul);
                SqlParameter SubCategory_id = new SqlParameter("@SubCategory_id", DBNull.Value);
                if (entity.SubCategory_id != null)
                {
                    SubCategory_id = new SqlParameter("@SubCategory_id", entity.SubCategory_id);

                }
                SqlParameter isExist = new SqlParameter("@isExist", entity.isExist);
                SqlParameter isDisplay = new SqlParameter("@isDisplay", entity.isDisplay);
                SqlParameter isPromotion = new SqlParameter("@isPromotion", entity.isPromotion);
                SqlParameter Promotion_Price = new SqlParameter("@Promotion_Price", DBNull.Value);
                if (entity.Promotion_Price != null)
                {
                    Promotion_Price = new SqlParameter("@Promotion_Price", entity.Promotion_Price);

                }




                _db.Database.ExecuteSqlRaw("EXECUTE UpdateProduct @Product_id, @Name_UKR,@Name_ENG,@Description_UKR,@Description_ENG,@Price,@Category_id,@Articul,@SubCategory_id,@isExist,@isDisplay, @isPromotion, @Promotion_Price ",
                Product_id, Name_UKR, Name_ENG, Description_UKR, Description_ENG, Price, Category_id, Articul, SubCategory_id, isExist, isDisplay, isPromotion , Promotion_Price);

                var product = _imageService.AddImageToProduct(entity);

                if (product.Images != null) {

                    foreach (var item in product.Images)
                    {
                        if (item.Image_id == 0)
                        {
                            _db.Product_images.Add(new Product_images
                            {
                                Image_id = item.Image_id,
                                Product_id = entity.Product_id,
                                Url = item.Url,
                                Alt_text = item.Alt_text

                            });
                        }
                    }
                    _db.SaveChanges();
                }

                var productTochange = _db.Products.Include(p => p.Attributes).FirstOrDefault(p => p.Product_id == entity.Product_id);
                List<int> OldAtributes = productTochange.Attributes.Select(a => a.Atribute_id).ToList();

                foreach (int item in OldAtributes)
                {
                    if (!entity.Attributes.Any(a => a.Atribute_id == item))
                    {
                        _atributeServise.DeleteAtribute(item);
                    }

                }


                if (entity.Attributes != null)
                {
                    foreach (var item in entity.Attributes)
                    {
                        if (item.Atribute_id == 0)
                        {
                            _atributeServise.AddAtribute(item);
                        }
                    }

                    foreach (var item in entity.Attributes)
                    {
                        if (item.Atribute_id != 0)
                        {
                            _atributeServise.UpdateAtribute(item);
                        }
                    }
                }

            }
            else
            {
                throw new J_NotFoundExeption();
            }
        }
    }
}
