using Jewelery.Models.Product_model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.data.FluentConfiguration.Product_model_configuration
{
    public class Atribute_Fluent : IEntityTypeConfiguration<Atribute>
    {
        public void Configure(EntityTypeBuilder<Atribute> builder)
        {

            //Name of table

            //Name of Colums

            //Primary Key
            builder.HasKey(a => a.Atribute_id);

            //Other Validation
            //Relationship
            builder.HasOne(a => a.Product).WithMany(p => p.Attributes).HasForeignKey(a => a.Product_id);
            builder.HasOne(a => a.DetermineTheSize).WithMany().HasForeignKey(a => a.DetermineTheSize_Id);


        }
    }
}