using Jewelery.data;
using Jewelery.Infrastructure.Binder.ProductBinder;
using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Jewelery.Models.Product_model;
using Jewelery.Servise.CategoryServise;
using Jewelery.Servise.ProductServise;
using Jewelery.ViewModels.DTO.Atribute;
using Jewelery.ViewModels.DTO.CMSFilter;
using Jewelery.ViewModels.DTO.Product;
using Jewelery.ViewModels.DTO.Product_images;
using Jewelery.ViewModels.VMCMS.VMCProduct.VMProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Jewelery.Controllers.CMS
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductController : Controller
    {
        private readonly AppDBContext _db;
        private readonly IProductServise _productServise;
        private readonly ICategoryServise _categoryServise;
        private const string FilteKey = "FilterProductCMS";


        public ProductController(AppDBContext db, IProductServise productServise, ICategoryServise categoryServise)
        {
            _db = db;
            _productServise = productServise;
            _categoryServise = categoryServise;
        }

        public IActionResult Product()
        {
            if (HttpContext.Session.GetString(FilteKey).IsNullOrEmpty()) 
            {
                ProductCMSFilter Createfilter = new ProductCMSFilter(_categoryServise);
                ProductCMSFilterDTO filter = new ProductCMSFilterDTO
                {
                    SelectedCategory = Createfilter.SelectedCategory,
                    isDispay = Createfilter.isDispay,
                    isExist = Createfilter.isExist,
                    isPromotion = Createfilter.isPromotion,
                    SearchString = ""
                };
                HttpContext.Session.SetString(FilteKey, JsonSerializer.Serialize(filter));
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult Product(string SearchTerm)
        {
            ProductCMSFilterDTO filter = JsonSerializer.Deserialize<ProductCMSFilterDTO>(HttpContext.Session.GetString(FilteKey));
            filter.SearchString = SearchTerm;
            HttpContext.Session.SetString(FilteKey, JsonSerializer.Serialize(filter));
            return View();
        }

        [HttpPost]
        public IActionResult SetSortVariable([FromBody] ProductCMSFilterDTO filter)
        {
            HttpContext.Session.SetString(FilteKey, JsonSerializer.Serialize(filter));

            return null;
        }

        public IActionResult ResetFilters()
        {
            HttpContext.Session.Remove(FilteKey);

            return RedirectToAction(nameof(Product));
        }

        public IActionResult AddProduct()
        {
            var model = new ProductCMSDTO
            {
                Images = new List<Product_imagesDTO>(),
                Attributes = new List<AtributeCMSDTO>()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult AddProduct([ProductCMSDTOBinder] ProductCMSDTO entity)
        {


            _productServise.Add(entity);
            return Ok(Url.Action("Product"));
            
           
        }

        public IActionResult EditProduct(int id)
        {
            ProductCMSDTO EntiryToEdit = _productServise.GetByIdCMS(id);
            if (EntiryToEdit.Images == null) 
            {
                EntiryToEdit.Images = new List<Product_imagesDTO>();
            }
            if (EntiryToEdit.Attributes == null)
            {
                EntiryToEdit.Attributes = new List<AtributeCMSDTO>();
            }
            return View(EntiryToEdit);
        }

        [HttpPost]
        public IActionResult EditProduct([ProductCMSDTOBinder] ProductCMSDTO entity)
        {
            _productServise.Update(entity);
            return Ok(Url.Action("Product"));
        }

        public IActionResult DeleteProduct(int id)
        {
            _productServise.Delete(id);
            return RedirectToAction("Product");
        }


    }
}
