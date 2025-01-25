﻿using Jewelery.Models.ConstantModel;
using Jewelery.Models.Product_model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jewelery.data.FluentConfiguration.Product_model_configuration
{
    public class ConstantCategory_Fluent : IEntityTypeConfiguration<ConstantCategory>
    {
        public void Configure(EntityTypeBuilder<ConstantCategory> builder)
        {
            //Name of table
            //Name of Colums
            //Primary Key
            builder.HasKey(c => c.Category_id);
            //Other Validation
            //Relationship
        }
    }
}
