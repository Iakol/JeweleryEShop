using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Jewelery.Models.Cart_Model;

namespace Jewelery.data.FluentConfiguration.Cart_model_configuration
{
    public class Cart_Fluent : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(c => c.Cart_id);
            //Other Validation
            //Relationship

            // Add relationship with User entity
        }
    }
}
