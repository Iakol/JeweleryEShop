using Jewelery.Models.Product_model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Jewelery.data.FluentConfiguration.Product_model_configuration
{
    public class DetermineTheSize_Fluent : IEntityTypeConfiguration<DetermineTheSize>
    {
        public void Configure(EntityTypeBuilder<DetermineTheSize> builder)
        {

            //Name of table

            //Name of Colums

            //Primary Key
            builder.HasKey(a => a.DetermineTheSize_Id);

            //Other Validation

            //Relationship
            


        }
    }
}