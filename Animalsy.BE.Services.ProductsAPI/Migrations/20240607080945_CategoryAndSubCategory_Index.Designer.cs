﻿// <auto-generated />
using System;
using Animalsy.BE.Services.ProductAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Animalsy.BE.Services.ProductAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240607080945_CategoryAndSubCategory_Index")]
    partial class CategoryAndSubCategory_Index
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Animalsy.BE.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryAndSubCategory")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<decimal>("MaxPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PromoPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("VendorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryAndSubCategory")
                        .HasDatabaseName("IX_Product_CategoryAndSubCategory");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
