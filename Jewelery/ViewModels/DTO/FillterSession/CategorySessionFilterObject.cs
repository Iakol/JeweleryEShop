using Jewelery.ViewModels.DTO.Category;

namespace Jewelery.ViewModels.DTO.FillterSession
{
    public class CategorySessionFilterObject
    {
        public int? currentPage {get;set;}
        public int? CategoryId { get;set;}
        public int? SubCategoryId { get;set;}
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }

        public decimal? MaxPrice { get;set;}
        public decimal? MinPrice { get;set;}

        public decimal? SelectMaxPrice { get; set; }
        public decimal? SelectMinPrice { get; set; }

        public string? Constanta { get;set;}
        public int? PageCount { get; set; }

        public bool? isExist { get; set; }

        public int OrderSelect { get; set; } = 0;

        public List<CategoryDTO> SubCategoryList { get; set; }

        public bool ListChangeFlag { get; set; } = false;


    }
}
