using Jewelery.data.FluentConfiguration.Cart_model_configuration;
using Jewelery.data.FluentConfiguration.Order_model_configuration;
using Jewelery.data.FluentConfiguration.Product_model_configuration;
using Jewelery.data.FluentConfiguration.Review_model_configuration;
using Jewelery.Infrastructure;
using Jewelery.Models.Cart_Model;
using Jewelery.Models.ConstantModel;
using Jewelery.Models.Identity_model;
using Jewelery.Models.Order_model;
using Jewelery.Models.Product_model;
using Jewelery.Models.Review;
using Jewelery.ViewModels.DTO.Atribute;
using Jewelery.ViewModels.DTO.Cart_item_option;
using Jewelery.ViewModels.DTO.Category;
using Jewelery.ViewModels.DTO.DetermineTheSizeEditor;
using Jewelery.ViewModels.DTO.Order;
using Jewelery.ViewModels.DTO.Order_detail;
using Jewelery.ViewModels.DTO.Product;
using Jewelery.ViewModels.DTO.SubCategory;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.data
{
    public class AppDBContext : IdentityDbContext<User>
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cart_item> Carts_items { get; set; }

        public DbSet<Cart_item_option> Cart_item_options { get; set; }


        public DbSet<Delivery_detail> Delivery_details { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_detail> Order_details { get; set; }
        public DbSet<Order_detail_option> Order_detail_options { get; set; }


        public DbSet<Atribute> Atributes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_images> Product_images { get; set; }
        public DbSet<Option> Options { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Fashion> Fashions { get; set; }


        public DbSet<DetermineTheSize> DetermineTheSizes { get; set; }
        public DbSet<DetermineTheSizeObject> DetermineTheSizeObjects { get; set; }





        public DbSet<LocalizationModel> Localizations { get; set; }

        public DbSet<ConstantCategory> ConstantCategories { get; set; }



        public AppDBContext(DbContextOptions options) : base(options)
        {
            Console.WriteLine("Initializing AppDBContext");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Cart_Fluent());
            modelBuilder.ApplyConfiguration(new Cart_item_Fluent());
            modelBuilder.ApplyConfiguration(new Cart_item_option_Fluent());


            modelBuilder.ApplyConfiguration(new Delivery_detail_configuration());
            modelBuilder.ApplyConfiguration(new Order_configuration());
            modelBuilder.ApplyConfiguration(new Order_detail_configuration());
            modelBuilder.ApplyConfiguration(new Order_detail_option_Fluent());


            modelBuilder.ApplyConfiguration(new Atribute_Fluent());
            modelBuilder.ApplyConfiguration(new Category_Fluent());
            modelBuilder.ApplyConfiguration(new SubCategory_Fluent());
            modelBuilder.ApplyConfiguration(new Product_Fluent());
            modelBuilder.ApplyConfiguration(new Product_images_Fluent());
            modelBuilder.ApplyConfiguration(new Option_Fluent());

            modelBuilder.ApplyConfiguration(new Review_Fluent());
            modelBuilder.ApplyConfiguration(new FAQ_Fluent());
            modelBuilder.ApplyConfiguration(new Fashion_Fluent());


            modelBuilder.ApplyConfiguration(new DetermineTheSize_Fluent());
            modelBuilder.ApplyConfiguration(new DetermineTheSizeObject_Fluent());



            modelBuilder.ApplyConfiguration(new Localization_Fluent());

            modelBuilder.ApplyConfiguration(new ConstantCategory_Fluent());


            //View Model

            modelBuilder.Entity<CategoryCMSDTO>().Ignore(c => c.ImageFile);
            modelBuilder.Entity<CategoryCMSDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<CategoryVMDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<SubCategoryVMDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<SubCategoryCMSDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<CategoryDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<AtributeDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<DetermineTheSizePageDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<ProductDTOVMPage>().HasNoKey().ToView(null);
            modelBuilder.Entity<ProductCMSDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<Cart_item_optionDTOVM>().HasNoKey().ToView(null);
            modelBuilder.Entity<AtributeCMSDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<OrderVMDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<Order_detailVMDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<Order_detail_optionVMDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<Delivery_detailVMDTO>().HasNoKey().ToView(null);

            





























        }

       



    }
}
