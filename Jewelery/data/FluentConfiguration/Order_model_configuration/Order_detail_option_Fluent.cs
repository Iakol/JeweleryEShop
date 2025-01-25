using Jewelery.Models.Cart_Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.data.FluentConfiguration.Cart_model_configuration
{
    public class Order_detail_option_Fluent : IEntityTypeConfiguration<Order_detail_option>
    {
        public void Configure(EntityTypeBuilder<Order_detail_option> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(cio => cio.Order_detail_option_id);
            //Other Validation
            builder.Property(cio => cio.PriceAdjustment).HasColumnType("decimal(18, 2)");
            builder.Property(cio => cio.Size).HasColumnType("decimal(18, 2)");
            //Relationship
            builder.HasOne(cio => cio.Order_detail).WithMany(ci => ci.Options).HasForeignKey(ci => ci.Order_detail_id);

        }
    }
}
