﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SallesApp.Context;

#nullable disable

namespace SallesApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250307195812_addShoppingCart")]
    partial class addShoppingCart
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SallesApp.Models.ItemShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("ShoppingCartId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ItemShoppingCarts");
                });

            modelBuilder.Entity("SallesApp.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageThumbnailUrl")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<bool>("IsAvailable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("LongDescription")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<short>("ProductCategoryId")
                        .HasColumnType("smallint");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("SallesApp.Models.ProductCategory", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories", (string)null);
                });

            modelBuilder.Entity("SallesApp.Models.ItemShoppingCart", b =>
                {
                    b.HasOne("SallesApp.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SallesApp.Models.Product", b =>
                {
                    b.HasOne("SallesApp.Models.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("SallesApp.Models.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
