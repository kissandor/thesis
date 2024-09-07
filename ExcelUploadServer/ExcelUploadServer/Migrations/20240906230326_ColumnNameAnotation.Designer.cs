﻿// <auto-generated />
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
    [Migration("20240906230326_ColumnNameAnotation")]
    partial class ColumnNameAnotation
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
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CategoryName");

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
                        .HasColumnType("int")
                        .HasColumnName("CategoryId");

                    b.Property<string>("ComputerPartName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ComputerPartName");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ComputerParts");
                });

            modelBuilder.Entity("ExcelUploadServer.Models.SearchResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ComputerPartId")
                        .HasColumnType("int")
                        .HasColumnName("ComputerPartId");

                    b.Property<decimal?>("ComputerPartPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Currency");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("ProductUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProductUrl");

                    b.Property<int>("WebShopId")
                        .HasColumnType("int")
                        .HasColumnName("WebShopId");

                    b.HasKey("Id");

                    b.HasIndex("ComputerPartId");

                    b.HasIndex("WebShopId");

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
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("WebShopName");

                    b.Property<string>("WebShopURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("WebShopURL");

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

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ExcelUploadServer.Models.SearchResult", b =>
                {
                    b.HasOne("ExcelUploadServer.Models.ComputerPart", "ComputerPart")
                        .WithMany()
                        .HasForeignKey("ComputerPartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExcelUploadServer.Models.WebShop", "WebShop")
                        .WithMany()
                        .HasForeignKey("WebShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ComputerPart");

                    b.Navigation("WebShop");
                });
#pragma warning restore 612, 618
        }
    }
}
