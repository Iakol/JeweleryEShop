using Jewelery.Models.Order_model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.data.FluentConfiguration.Order_model_configuration
{
    public class Order_detail_configuration : IEntityTypeConfiguration<Order_detail>
    {
        public void Configure(EntityTypeBuilder<Order_detail> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(od => od.Order_detail_id);
            //Other Validation
            //Relationship
            builder.HasOne(od => od.Order).WithMany(o => o.Order_Details).HasForeignKey(od => od.Order_id);
            builder.HasOne(od => od.Product).WithMany().HasForeignKey(od => od.Product_id);

        }
    }
}