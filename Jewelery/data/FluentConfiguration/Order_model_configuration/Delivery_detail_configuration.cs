using Jewelery.Models.Product_model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Jewelery.Models.Order_model;

namespace Jewelery.data.FluentConfiguration.Order_model_configuration
{
    public class Delivery_detail_configuration : IEntityTypeConfiguration<Delivery_detail>
    {
        public void Configure(EntityTypeBuilder<Delivery_detail> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(dd => dd.Delivery_detail_id);
            //Other Validation
            //Relationship
            builder.HasOne(dd => dd.Order).WithOne(o => o.Delivery_detail).HasForeignKey<Delivery_detail>(dd => dd.Order_id);
        }
    }
}