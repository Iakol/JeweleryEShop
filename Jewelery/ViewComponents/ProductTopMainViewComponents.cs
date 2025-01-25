using Jewelery.data;
using Jewelery.Servise.ProductServise;
using Jewelery.ViewModels.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Jewelery.ViewComponents
{
    public class ProductTopMainViewComponent : ViewComponent
    {
        private readonly AppDBContext _db;
        private readonly IProductServise _productServise;

        public ProductTopMainViewComponent(AppDBContext db, IProductServise productServise)
        {
            _db = db;
            _productServise = productServise;
        }

        public async Task<IViewComponentResult> InvokeAsync(int lang) 
        {

            var list = GetSort(lang);
            return View(list);
        }

        public List<ProductDTOVMPage> GetSort(int lang) 
        {
            var list = _productServise.GetAll(lang).Where(p => p.isExist).Take(10).ToList();
  

            return list;
        }
    }
}
