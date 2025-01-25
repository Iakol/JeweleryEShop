﻿// <auto-generated />
using System;
using Jewelery.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Jewelery.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240708183243_UpdateDbSQL")]
    partial class UpdateDbSQL
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Jewelery.Infrastructure.LocalizationModel", b =>
                {
                    b.Property<int>("Value_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Value_Id"));

                    b.Property<string>("ENG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type_Variable")
                        .HasColumnType("int");

                    b.Property<string>("UKR")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Value_Id");

                    b.ToTable("Localizations");
                });

            modelBuilder.Entity("Jewelery.Models.Cart_Model.Cart", b =>
                {
                    b.Property<int>("Cart_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cart_id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Session_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Update_at")
                        .HasColumnType("datetime2");

                    b.HasKey("Cart_id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Jewelery.Models.Cart_Model.Cart_item", b =>
                {
                    b.Property<int>("Item_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Item_id"));

                    b.Property<int>("Cart_id")
                        .HasColumnType("int");

                    b.Property<int>("Product_id")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Item_id");

                    b.HasIndex("Cart_id");

                    b.HasIndex("Product_id");

                    b.ToTable("Carts_items");
                });

            modelBuilder.Entity("Jewelery.Models.Identity_model.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Father_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Phone_number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Second_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Jewelery.Models.Order_model.Delivery_detail", b =>
                {
                    b.Property<int>("Delivery_detail_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Delivery_detail_id"));

                    b.Property<string>("Delivery_Father_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Delivery_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Delivery_Second_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order_id")
                        .HasColumnType("int");

                    b.Property<string>("Phone_number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Delivery_detail_id");

                    b.HasIndex("Order_id")
                        .IsUnique();

                    b.ToTable("Delivery_details");
                });

            modelBuilder.Entity("Jewelery.Models.Order_model.Order", b =>
                {
                    b.Property<int>("Order_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Order_id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Total_price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Update_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("User_id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Order_id");

                    b.HasIndex("User_id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Jewelery.Models.Order_model.Order_detail", b =>
                {
                    b.Property<int>("Order_detail_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Order_detail_id"));

                    b.Property<int>("Order_id")
                        .HasColumnType("int");

                    b.Property<int>("Product_id")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Order_detail_id");

                    b.HasIndex("Order_id");

                    b.HasIndex("Product_id");

                    b.ToTable("Order_details");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Atribute", b =>
                {
                    b.Property<int>("Atribute_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Atribute_id"));

                    b.Property<int>("Atribute_name")
                        .HasColumnType("int");

                    b.Property<int?>("DetermineTheSize_Id")
                        .HasColumnType("int");

                    b.Property<int>("Product_id")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Atribute_id");

                    b.HasIndex("DetermineTheSize_Id");

                    b.HasIndex("Product_id");

                    b.ToTable("Atributes");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Category", b =>
                {
                    b.Property<int>("Category_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Category_id"));

                    b.Property<int>("Description")
                        .HasColumnType("int");

                    b.Property<int>("Image")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Category_id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.DetermineTheSize", b =>
                {
                    b.Property<int>("DetermineTheSize_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetermineTheSize_Id"));

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DetermineTheSize_Id");

                    b.ToTable("DetermineTheSizes");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Option", b =>
                {
                    b.Property<int>("Option_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Option_id"));

                    b.Property<int>("Atribute_id")
                        .HasColumnType("int");

                    b.Property<int?>("Cart_itemItem_id")
                        .HasColumnType("int");

                    b.Property<int?>("Order_detail_id")
                        .HasColumnType("int");

                    b.Property<decimal>("PriceAdjustment")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Size")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Option_id");

                    b.HasIndex("Atribute_id");

                    b.HasIndex("Cart_itemItem_id");

                    b.HasIndex("Order_detail_id");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Product", b =>
                {
                    b.Property<int>("Product_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Product_id"));

                    b.Property<int>("Articul")
                        .HasColumnType("int");

                    b.Property<int>("Category_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<int>("Description")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SubCategory_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated_at")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isDisplay")
                        .HasColumnType("bit");

                    b.Property<bool>("isExist")
                        .HasColumnType("bit");

                    b.HasKey("Product_id");

                    b.HasIndex("Category_id");

                    b.HasIndex("SubCategory_id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Product_images", b =>
                {
                    b.Property<int>("Image_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Image_id"));

                    b.Property<string>("Alt_text")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Product_id")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Image_id");

                    b.HasIndex("Product_id");

                    b.ToTable("Product_images");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.SubCategory", b =>
                {
                    b.Property<int>("SubCategory_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategory_id"));

                    b.Property<int>("Category_id")
                        .HasColumnType("int");

                    b.Property<int>("Description")
                        .HasColumnType("int");

                    b.Property<int>("Image")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("int");

                    b.HasKey("SubCategory_id");

                    b.HasIndex("Category_id");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("Jewelery.Models.Review.Review", b =>
                {
                    b.Property<int>("Review_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Review_id"));

                    b.Property<int>("Product_id")
                        .HasColumnType("int");

                    b.Property<int>("Raiting")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Review_id");

                    b.HasIndex("Product_id")
                        .IsUnique();

                    b.HasIndex("User_id")
                        .IsUnique()
                        .HasFilter("[User_id] IS NOT NULL");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Jewelery.ViewModels.DTO.Category.CategoryDTO", b =>
                {
                    b.Property<int>("Category_id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Image")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CategoryDTO");
                });

            modelBuilder.Entity("Jewelery.ViewModels.DTO.Category.CategoryVMDTO", b =>
                {
                    b.Property<int>("Category_id")
                        .HasColumnType("int");

                    b.Property<int>("Image")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CategoryVMDTO");
                });

            modelBuilder.Entity("Jewelery.ViewModels.DTO.SubCategory.SubCategoryVMDTO", b =>
                {
                    b.Property<int>("Image")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubCategory_id")
                        .HasColumnType("int");

                    b.ToTable("SubCategoryVMDTO");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Jewelery.Models.Cart_Model.Cart_item", b =>
                {
                    b.HasOne("Jewelery.Models.Cart_Model.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("Cart_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jewelery.Models.Product_model.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Jewelery.Models.Order_model.Delivery_detail", b =>
                {
                    b.HasOne("Jewelery.Models.Order_model.Order", "Order")
                        .WithOne()
                        .HasForeignKey("Jewelery.Models.Order_model.Delivery_detail", "Order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Jewelery.Models.Order_model.Order", b =>
                {
                    b.HasOne("Jewelery.Models.Identity_model.User", "User")
                        .WithMany()
                        .HasForeignKey("User_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Jewelery.Models.Order_model.Order_detail", b =>
                {
                    b.HasOne("Jewelery.Models.Order_model.Order", "Order")
                        .WithMany("Order_Details")
                        .HasForeignKey("Order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jewelery.Models.Product_model.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Atribute", b =>
                {
                    b.HasOne("Jewelery.Models.Product_model.DetermineTheSize", "DetermineTheSize")
                        .WithMany()
                        .HasForeignKey("DetermineTheSize_Id");

                    b.HasOne("Jewelery.Models.Product_model.Product", "Product")
                        .WithMany("Attributes")
                        .HasForeignKey("Product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DetermineTheSize");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Option", b =>
                {
                    b.HasOne("Jewelery.Models.Product_model.Atribute", "Atribute")
                        .WithMany("Options")
                        .HasForeignKey("Atribute_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jewelery.Models.Cart_Model.Cart_item", null)
                        .WithMany("Options")
                        .HasForeignKey("Cart_itemItem_id");

                    b.HasOne("Jewelery.Models.Order_model.Order_detail", null)
                        .WithMany("Options")
                        .HasForeignKey("Order_detail_id");

                    b.Navigation("Atribute");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Product", b =>
                {
                    b.HasOne("Jewelery.Models.Product_model.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("Category_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Jewelery.Models.Product_model.SubCategory", "SubCategory")
                        .WithMany("Products")
                        .HasForeignKey("SubCategory_id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Category");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Product_images", b =>
                {
                    b.HasOne("Jewelery.Models.Product_model.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("Product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.SubCategory", b =>
                {
                    b.HasOne("Jewelery.Models.Product_model.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("Category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Jewelery.Models.Review.Review", b =>
                {
                    b.HasOne("Jewelery.Models.Product_model.Product", "Product")
                        .WithOne()
                        .HasForeignKey("Jewelery.Models.Review.Review", "Product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jewelery.Models.Identity_model.User", "User")
                        .WithOne()
                        .HasForeignKey("Jewelery.Models.Review.Review", "User_id");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Jewelery.Models.Identity_model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Jewelery.Models.Identity_model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jewelery.Models.Identity_model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Jewelery.Models.Identity_model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Jewelery.Models.Cart_Model.Cart_item", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("Jewelery.Models.Order_model.Order", b =>
                {
                    b.Navigation("Order_Details");
                });

            modelBuilder.Entity("Jewelery.Models.Order_model.Order_detail", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Atribute", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Category", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.Product", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("Jewelery.Models.Product_model.SubCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
