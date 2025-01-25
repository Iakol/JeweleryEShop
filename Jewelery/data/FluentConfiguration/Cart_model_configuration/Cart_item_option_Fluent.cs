using Jewelery.Models.Cart_Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.data.FluentConfiguration.Cart_model_configuration
{
    public class Cart_item_option_Fluent : IEntityTypeConfiguration<Cart_item_option>
    {
        public void Configure(EntityTypeBuilder<Cart_item_option> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(cio => cio.Cart_item_option_id);
            //Other Validation
            builder.Property(cio => cio.PriceAdjustment).HasColumnType("decimal(18, 2)");
            builder.Property(cio => cio.Size).HasColumnType("decimal(18, 2)");

            //Relationship
            builder.HasOne(cio => cio.cart_Item).WithMany(ci => ci.Options).HasForeignKey(ci => ci.Cart_item_id);

        }
    }
}
