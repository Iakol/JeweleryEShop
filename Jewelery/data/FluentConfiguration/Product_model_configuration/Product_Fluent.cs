using Jewelery.Models.Product_model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jewelery.data.FluentConfiguration.Product_model_configuration
{
    public class Product_Fluent : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            //Name of table

            //Name of Colums

            //Primary Key
            builder.HasKey(p => p.Product_id);

            //Other Validation
            builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.Property(p => p.Promotion_Price).HasPrecision(18, 2);

            //Relationship
            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.Category_id).OnDelete(DeleteBehavior.NoAction); ;
            builder.HasOne(p => p.SubCategory).WithMany(c => c.Products).HasForeignKey(p => p.SubCategory_id).OnDelete(DeleteBehavior.NoAction); 


        }
    }
}

//Name of table
//Name of Colums
//Primary Key
//Other Validation
//Relationship