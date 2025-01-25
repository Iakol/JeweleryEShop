using Jewelery.Models.Product_model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jewelery.data.FluentConfiguration.Product_model_configuration
{
    public class Product_images_Fluent : IEntityTypeConfiguration<Product_images>
    {
        public void Configure(EntityTypeBuilder<Product_images> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(pi => pi.Image_id);
            //Other Validation
            builder.Property(pi => pi.Alt_text).HasMaxLength(255).IsRequired();

            //Relationship
            builder.HasOne(pi => pi.Product).WithMany(p => p.Images).HasForeignKey(p => p.Product_id);
        }
    }
}
