using Jewelery.Models.Order_model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.data.FluentConfiguration.Order_model_configuration
{
    public class Order_configuration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(o => o.Order_id);
            //Other Validation
            builder.Property(p => p.Total_price).HasPrecision(18, 2);
            //Relationship
            builder.HasOne(o => o.User).WithMany().HasForeignKey(o => o.User_id);


        }
    }
}