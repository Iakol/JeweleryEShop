using Jewelery.Models.Product_model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jewelery.data.FluentConfiguration.Product_model_configuration
{
    public class SubCategory_Fluent : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
         // Name of table
        //Name of Colums
        //Primary Key
        builder.HasKey(c => c.SubCategory_id);
        //Other Validation
        builder.Property(c => c.Name).HasMaxLength(255).IsRequired();
        //Relationship
        builder.HasOne(sc => sc.Category).WithMany(c => c.SubCategories).HasForeignKey(c => c.Category_id);
        }
        
    }
}
