using Jewelery.Models.Product_model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Jewelery.Models.Review;

namespace Jewelery.data.FluentConfiguration.Review_model_configuration
{
    public class Review_Fluent : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(r => r.Review_id);
            //Other Validation
            //Relationship
            builder.HasOne(r => r.User).WithOne().HasForeignKey<Review>(r => r.User_id);
            builder.HasOne(r => r.Product).WithOne().HasForeignKey<Review>(r => r.Product_id);

        }
    }
}
