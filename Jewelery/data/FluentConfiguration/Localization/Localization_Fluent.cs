using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Jewelery.Infrastructure;

namespace Jewelery.data.FluentConfiguration.Cart_model_configuration
{
    public class Localization_Fluent : IEntityTypeConfiguration<LocalizationModel>
    {
        public void Configure(EntityTypeBuilder<LocalizationModel> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(l => l.Value_Id);
            //Other Validation
            
            //Relationship

            // Add relationship with User entity
        }
    }
}
