﻿// <auto-generated />
using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(StoreDBContext))]
    [Migration("20231107100642_Initial_DB")]
    partial class Initial_DB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataAccess.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Television"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Telephone"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Laptop"
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryName = "Smart Watch"
                        });
                });

            modelBuilder.Entity("DataAccess.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RequireDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ShippedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            OrderDate = new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RequireDate = new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ShippedDate = new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("DataAccess.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"), 1L, 1);

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");

                    b.HasData(
                        new
                        {
                            OrderDetailId = 1,
                            Discount = 20,
                            OrderId = 1,
                            Price = 800m,
                            ProductId = 1,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("DataAccess.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            Image = "image01.jpg",
                            ProductName = "TV SAMSUNG VOLET 50' 4K",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            Image = "image02.jpg",
                            ProductName = "TV SAMSUNG VOLET 45' 4k",
                            Quantity = 20,
                            UnitPrice = 500m
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 1,
                            Image = "image03.jpg",
                            ProductName = "TV SAMSUNG VOLET 50' 2K",
                            Quantity = 20,
                            UnitPrice = 400m
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 1,
                            Image = "image04.jpg",
                            ProductName = "TV SONY Bravia 50' 4K",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 1,
                            Image = "image05.jpg",
                            ProductName = "TV SAMSUNG QOLET 50' 4K",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 1,
                            Image = "image06.jpg",
                            ProductName = "TV SAMSUNG QOLET 50' 2K",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 1,
                            Image = "image07.jpg",
                            ProductName = "Google TV Sony 4K 43 inch KD-43X77L",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 1,
                            Image = "image08.jpg",
                            ProductName = "Smart TV NanoCell LG 4K 65 inch 65NANO76SQA",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 1,
                            Image = "image09.jpg",
                            ProductName = "Smart TV LG 4K 55 inch 55UQ8000PSC",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 1,
                            Image = "image10.jpg",
                            ProductName = "Smart TV OLED LG 4K 55 inch 55C2PSA",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 11,
                            CategoryId = 2,
                            Image = "image11.jpg",
                            ProductName = "iPhone 15 Pro Max (512GB)",
                            Quantity = 20,
                            UnitPrice = 1500m
                        },
                        new
                        {
                            ProductId = 12,
                            CategoryId = 2,
                            Image = "image12.jpg",
                            ProductName = "iPhone 15 Plus (128GB)",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 13,
                            CategoryId = 2,
                            Image = "image13.jpg",
                            ProductName = "Samsung Galaxy Z Fold5 12GB/256GB",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 14,
                            CategoryId = 2,
                            Image = "image14.jpg",
                            ProductName = "Samsung Galaxy Z Flip5 8GB/256GB ",
                            Quantity = 20,
                            UnitPrice = 800m
                        },
                        new
                        {
                            ProductId = 15,
                            CategoryId = 2,
                            Image = "image15.jpg",
                            ProductName = "OPPO Find N3 Flip (12GB/256GB)",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 16,
                            CategoryId = 2,
                            Image = "image16.jpg",
                            ProductName = "OPPO Reno10 5G (8GB/256GB)",
                            Quantity = 20,
                            UnitPrice = 500m
                        },
                        new
                        {
                            ProductId = 17,
                            CategoryId = 2,
                            Image = "image17.jpg",
                            ProductName = "Xiaomi 13T (12GB/256GB)",
                            Quantity = 20,
                            UnitPrice = 400m
                        },
                        new
                        {
                            ProductId = 18,
                            CategoryId = 2,
                            Image = "image18.jpg",
                            ProductName = "Xiaomi 13 Lite (8GB/256GB)",
                            Quantity = 20,
                            UnitPrice = 300m
                        },
                        new
                        {
                            ProductId = 19,
                            CategoryId = 2,
                            Image = "image19.jpg",
                            ProductName = "TECNO CAMON 20 Pro (8GB/256GB)",
                            Quantity = 20,
                            UnitPrice = 200m
                        },
                        new
                        {
                            ProductId = 20,
                            CategoryId = 2,
                            Image = "image20.jpg",
                            ProductName = "TECNO CAMON 20 (8GB/256GB)",
                            Quantity = 20,
                            UnitPrice = 300m
                        },
                        new
                        {
                            ProductId = 21,
                            CategoryId = 3,
                            Image = "image21.jpg",
                            ProductName = "Laptop Dell Vostro 3520-V5I3614W1 (i3-1215U/8GB/256GB/15.6 FHD/Windows 11+ Office)",
                            Quantity = 20,
                            UnitPrice = 600m
                        },
                        new
                        {
                            ProductId = 22,
                            CategoryId = 3,
                            Image = "image22.jpg",
                            ProductName = "Laptop Dell Inspiron 16 5625 99VP91 R7-5825U/8GD4/512SSD/16.0FHD+/W11SL+OFFICE ST/BẠC",
                            Quantity = 20,
                            UnitPrice = 700m
                        },
                        new
                        {
                            ProductId = 23,
                            CategoryId = 3,
                            Image = "image23.jpg",
                            ProductName = "MacBook Air M2 15 (8GB/256GB)",
                            Quantity = 20,
                            UnitPrice = 1300m
                        },
                        new
                        {
                            ProductId = 24,
                            CategoryId = 3,
                            Image = "image24.jpg",
                            ProductName = "MacBook Pro 16 (M2 Pro/12-core CPU/19-core GPU/16GB/512GB)",
                            Quantity = 20,
                            UnitPrice = 2000m
                        },
                        new
                        {
                            ProductId = 25,
                            CategoryId = 3,
                            Image = "image25.jpg",
                            ProductName = "Laptop ASUS TUF Gaming F15 FX507ZC4-HN099W (i7-12700H/8GB/512GB/RTX 3050/15.6 FHD 144Hz/Windows 11)",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 26,
                            CategoryId = 3,
                            Image = "image26.jpg",
                            ProductName = "Laptop ASUS VivoBook Go 14 E1404FA-NK177W (R5-7520U/16GB/512GB/14 FHD/Windows 11)",
                            Quantity = 20,
                            UnitPrice = 600m
                        },
                        new
                        {
                            ProductId = 27,
                            CategoryId = 3,
                            Image = "image27.jpg",
                            ProductName = "Laptop Lenovo IdeaPad Slim 3 14IAH8 83EQ0005VN",
                            Quantity = 20,
                            UnitPrice = 600m
                        },
                        new
                        {
                            ProductId = 28,
                            CategoryId = 3,
                            Image = "image28.jpg",
                            ProductName = "Laptop Lenovo Thinkbook 16P G2 ACH 20YM003JVN",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 29,
                            CategoryId = 3,
                            Image = "image29.jpg",
                            ProductName = "Laptop Gaming Acer Nitro 5 Eagle AN515-57-5669 NH.QEHSV.001",
                            Quantity = 20,
                            UnitPrice = 700m
                        },
                        new
                        {
                            ProductId = 30,
                            CategoryId = 3,
                            Image = "image30.jpg",
                            ProductName = "Laptop Gaming Acer Nitro 5 Tiger AN515 58 52SP",
                            Quantity = 20,
                            UnitPrice = 1000m
                        },
                        new
                        {
                            ProductId = 31,
                            CategoryId = 4,
                            Image = "image31.jpg",
                            ProductName = "Samsung Galaxy Watch5 40mm ",
                            Quantity = 20,
                            UnitPrice = 100m
                        },
                        new
                        {
                            ProductId = 32,
                            CategoryId = 4,
                            Image = "image32.jpg",
                            ProductName = "Apple Watch Ultra 2 LTE 49mm",
                            Quantity = 20,
                            UnitPrice = 1000m
                        });
                });

            modelBuilder.Entity("DataAccess.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Country = "Ha Noi",
                            Email = "abcd@gmail.com",
                            Password = "12345678",
                            Phone = "0921837232",
                            UserName = "Nguyen Thi A"
                        },
                        new
                        {
                            UserId = 2,
                            Country = "Ha Noi",
                            Email = "sdsds1234@gmail.com",
                            Password = "12345678",
                            Phone = "0921854321",
                            UserName = "Nguyen Van B"
                        });
                });

            modelBuilder.Entity("DataAccess.Models.Order", b =>
                {
                    b.HasOne("DataAccess.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Models.OrderDetail", b =>
                {
                    b.HasOne("DataAccess.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DataAccess.Models.Product", b =>
                {
                    b.HasOne("DataAccess.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DataAccess.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("DataAccess.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("DataAccess.Models.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("DataAccess.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
