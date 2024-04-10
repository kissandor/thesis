﻿// <auto-generated />
using System;
using ExcelUploadServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExcelUploadServer.Migrations
{
    [DbContext(typeof(ExcelUploadContext))]
    [Migration("20240402194407_AddSearchResultTableToDataBase")]
    partial class AddSearchResultTableToDataBase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExcelUploadServer.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ExcelUploadServer.Models.ComputerPart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ComputerPartName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ComputerPartPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("WebshopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("WebshopId");

                    b.ToTable("ComputerParts");
                });

            modelBuilder.Entity("ExcelUploadServer.Models.SearchResult", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ComputerPartName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ComputerPartPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WebshopId")
                        .HasColumnType("int");

                    b.HasIndex("CategoryId");

                    b.HasIndex("WebshopId");

                    b.ToTable("SearchResults");
                });

            modelBuilder.Entity("ExcelUploadServer.Models.WebShop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("WebShopName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebShopURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WebShops");
                });

            modelBuilder.Entity("ExcelUploadServer.Models.ComputerPart", b =>
                {
                    b.HasOne("ExcelUploadServer.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExcelUploadServer.Models.WebShop", "Webshop")
                        .WithMany()
                        .HasForeignKey("WebshopId");

                    b.Navigation("Category");

                    b.Navigation("Webshop");
                });

            modelBuilder.Entity("ExcelUploadServer.Models.SearchResult", b =>
                {
                    b.HasOne("ExcelUploadServer.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExcelUploadServer.Models.WebShop", "Webshop")
                        .WithMany()
                        .HasForeignKey("WebshopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Webshop");
                });
#pragma warning restore 612, 618
        }
    }
}
