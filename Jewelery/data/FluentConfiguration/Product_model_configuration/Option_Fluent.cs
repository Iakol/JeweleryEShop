using Jewelery.Models.Product_model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.data.FluentConfiguration.Product_model_configuration
{
    public class Option_Fluent : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(k => k.Option_id);
            //Other Validation
            builder.Property(p => p.Size).HasPrecision(18, 2);
            builder.Property(p => p.PriceAdjustment).HasPrecision(18, 2);
            //Relationship
            builder.HasOne(pa => pa.Atribute).WithMany(a => a.Options).HasForeignKey(pa => pa.Atribute_id);

        }
    }
}
