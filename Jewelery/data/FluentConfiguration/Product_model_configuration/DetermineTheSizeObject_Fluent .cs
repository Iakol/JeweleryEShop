using Jewelery.Models.Product_model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.data.FluentConfiguration.Product_model_configuration
{
    public class DetermineTheSizeObject_Fluent : IEntityTypeConfiguration<DetermineTheSizeObject>
    {
        public void Configure(EntityTypeBuilder<DetermineTheSizeObject> builder)
        {

            //Name of table

            //Name of Colums

            //Primary Key
            builder.HasKey(a => a.DetermineTheSizeObject_Id);

            //Other Validation

            //Relationship
            builder.HasOne(dO => dO.DetermineTheSize).WithMany(ds => ds.determineTheSizeObjects).HasForeignKey(dO => dO.DetermineTheSize_Id);
            


        }
    }
}