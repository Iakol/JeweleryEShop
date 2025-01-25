using Jewelery.Infrastructure.Exeption.CustomExeptionType;
using Jewelery.Models.Product_model;
using Jewelery.ViewModels.DTO.Atribute;
using Jewelery.ViewModels.DTO.Product;
using Jewelery.ViewModels.DTO.Product_images;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace Jewelery.Infrastructure.Binder.ProductBinder
{
    public class ProductCSMDTOBinder : IModelBinder
    {   
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) 
            {
                throw new J_BadRequestExeption("form is not exist");
            }

            var form = bindingContext.HttpContext.Request.Form;

            ProductCMSDTO product = new ProductCMSDTO
            {
                Product_id = int.Parse(form["Product_id"]),
                Name_UKR = form["Name_UKR"],
                Name_ENG = form["Name_ENG"],
                Description_UKR = form["Description_UKR"],
                Description_ENG = form["Description_ENG"],
                Price = getPrice(form["Price"]),
                Category_id = int.Parse(form["Category_id"]),
                SubCategory_id = int.Parse(form["SubCategory_id"]),
                Articul = int.Parse(form["Articul"]),
                Images = getProduct_imagesDTO(form),
                Attributes = getAtributeCMSDTO(form),
                isExist = bool.Parse(form["isExist"]),
                isDisplay = bool.Parse(form["isDisplay"]),
                isPromotion = bool.Parse(form["isPromotion"]),
                Promotion_Price = form["Promotion_Price"].IsNullOrEmpty()? null: getPrice(form["Promotion_Price"]) 

            };

            bindingContext.Result = ModelBindingResult.Success(product);
            return Task.CompletedTask;
        }

        private List<Option> getOption(IFormCollection form, int index) 
        {
            List < Option > options = new List<Option>();
            int OptionCount = int.Parse(form[$"Attributes[{index}].Count"]);
            for (int i = 0; i < OptionCount; i++) 
            {
                options.Add(new Option
                {
                    Option_id = int.Parse(form[$"Attributes[{index}].Options[{i}].Option_id"]),                    
                    Atribute_id = int.Parse(form[$"Attributes[{index}].Options[{i}].Atribute_id"]),
                    Size = getSize(form[$"Attributes[{index}].Options[{i}].Size"]),
                    PriceAdjustment = getPrice(form[$"Attributes[{index}].Options[{i}].PriceAdjustment"])
                });
            }

            return options;
        
        }
        private List<Product_imagesDTO> getProduct_imagesDTO(IFormCollection form)
        {
            List<Product_imagesDTO> Product_images = new List<Product_imagesDTO>();
            int ImageCounter = int.Parse(form["ImageCounter"]);
            for (int i = 0; i < ImageCounter; i++)
            {
                IFormFile formFile = form.Files.FirstOrDefault(f => f.Name == $"Images[{i}].ImageFile");
                Product_images.Add(new Product_imagesDTO
                {
                    Image_id = int.Parse(form[$"Images[{i}].Image_id"]),
                    Product_id = 0,                    
                    Alt_text = form[$"Images[{i}].Alt_text"],
                    ImageFile = formFile
                });
            }

            return Product_images;

        }
        private List<AtributeCMSDTO> getAtributeCMSDTO(IFormCollection form) 
        {
            List<AtributeCMSDTO> Atributes = new List<AtributeCMSDTO>();
            int AtributeCount = int.Parse(form["AttributesCount"]);
            for (int i = 0; i < AtributeCount; i++)
            {
                Atributes.Add(new AtributeCMSDTO
                {
                    Atribute_id = int.Parse(form[$"Attributes[{i}].Atribute_id"]),
                    Atribute_name_UKR = form[$"Attributes[{i}].Atribute_name_UKR"],
                    Atribute_name_ENG = form[$"Attributes[{i}].Atribute_name_ENG"],
                    Product_id = 0,
                    Unit = form[$"Attributes[{i}].Unit"].IsNullOrEmpty()? "" : form[$"Attributes[{i}].Unit"],
                    DetermineTheSize_Id = form[$"Attributes[{i}].DetermineTheSize_Id"].IsNullOrEmpty()? (int?)null : int.Parse(form[$"Attributes[{i}].DetermineTheSize_Id"]),
                    Options = getOption(form, i)
                });
            }

            return Atributes;

        }

        private decimal getPrice(string dec) 
        {
            decimal price;

            if (decimal.TryParse(dec, NumberStyles.None, CultureInfo.InvariantCulture, out price))
            {
                price = Math.Round(price, 2);
            }
            else 
            {
                price = 0;
            }

            return price;
        }

        private decimal getSize(string dec)
        {
            decimal size;

            if (decimal.TryParse(dec, NumberStyles.None, CultureInfo.InvariantCulture, out size))
            {
                size = Math.Round(size, 2);
            }
            else
            {
                size = 0;
            }

            return size;
        }
    }
}
