using Jewelery.Models.Cart_Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.data.FluentConfiguration.Cart_model_configuration
{
    public class Cart_item_Fluent : IEntityTypeConfiguration<Cart_item>
    {
        public void Configure(EntityTypeBuilder<Cart_item> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(ci => ci.Item_id);
            //Other Validation
            //Relationship
            builder.HasOne(ci => ci.Product).WithMany().HasForeignKey(ci => ci.Product_id);
            builder.HasOne(ci => ci.Cart).WithMany().HasForeignKey(ci => ci.Cart_id);

        }
    }
}
