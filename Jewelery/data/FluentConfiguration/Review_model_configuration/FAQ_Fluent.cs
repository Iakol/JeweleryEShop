using Jewelery.Models.Product_model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Jewelery.Models.Review;

namespace Jewelery.data.FluentConfiguration.Review_model_configuration
{
    public class FAQ_Fluent : IEntityTypeConfiguration<FAQ>
    {
        public void Configure(EntityTypeBuilder<FAQ> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(f => f.Id);
            //Other Validation
            //Relationship

        }
    }
}
