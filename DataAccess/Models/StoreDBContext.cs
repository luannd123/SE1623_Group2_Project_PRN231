using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json",optional:true,reloadOnChange:true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DbConn"));
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category { CategoryId = 1 , CategoryName = "Television"},
                    new Category { CategoryId = 2 , CategoryName = "Telephone"},
                    new Category { CategoryId = 3 , CategoryName = "Laptop"},
                    new Category { CategoryId = 4 , CategoryName = "Smart Watch"}
                );
            modelBuilder.Entity<Product>().HasData(
                    new Product { ProductId = 1, ProductName = "TV SAMSUNG VOLET 50' 4K", CategoryId = 1, UnitPrice = 1000, Quantity = 20, Image = "image01.jpg" },
                    new Product { ProductId = 2, ProductName = "TV SAMSUNG VOLET 45' 4k", CategoryId = 1, UnitPrice = 500, Quantity = 20, Image = "image02.jpg" },
                    new Product { ProductId = 3, ProductName = "TV SAMSUNG VOLET 50' 2K", CategoryId = 1, UnitPrice = 400, Quantity = 20, Image = "image03.jpg" },
                    new Product { ProductId = 4, ProductName = "TV SONY Bravia 50' 4K", CategoryId = 1, UnitPrice = 1000, Quantity = 20, Image = "image04.jpg" },
                    new Product { ProductId = 5, ProductName = "TV SAMSUNG QOLET 50' 4K", CategoryId = 1, UnitPrice = 1000, Quantity = 20, Image = "image05.jpg" },
                    new Product { ProductId = 6, ProductName = "TV SAMSUNG QOLET 50' 2K", CategoryId = 1, UnitPrice = 1000, Quantity = 20, Image = "image06.jpg" },
                    new Product { ProductId = 7, ProductName = "Google TV Sony 4K 43 inch KD-43X77L", CategoryId = 1, UnitPrice = 1000, Quantity = 20, Image = "image07.jpg" },
                    new Product { ProductId = 8, ProductName = "Smart TV NanoCell LG 4K 65 inch 65NANO76SQA", CategoryId = 1, UnitPrice = 1000, Quantity = 20, Image = "image08.jpg" },
                    new Product { ProductId = 9, ProductName = "Smart TV LG 4K 55 inch 55UQ8000PSC", CategoryId = 1, UnitPrice = 1000, Quantity = 20, Image = "image09.jpg" },
                    new Product { ProductId = 10, ProductName = "Smart TV OLED LG 4K 55 inch 55C2PSA", CategoryId = 1, UnitPrice = 1000, Quantity = 20, Image = "image10.jpg" },
                    new Product { ProductId = 11, ProductName = "iPhone 15 Pro Max (512GB)", CategoryId = 2, UnitPrice = 1500, Quantity = 20, Image = "image11.jpg" },
                    new Product { ProductId = 12, ProductName = "iPhone 15 Plus (128GB)", CategoryId = 2, UnitPrice = 1000, Quantity = 20, Image = "image12.jpg" },
                    new Product { ProductId = 13, ProductName = "Samsung Galaxy Z Fold5 12GB/256GB", CategoryId = 2, UnitPrice = 1000, Quantity = 20, Image = "image13.jpg" },
                    new Product { ProductId = 14, ProductName = "Samsung Galaxy Z Flip5 8GB/256GB ", CategoryId = 2, UnitPrice = 800, Quantity = 20, Image = "image14.jpg" },
                    new Product { ProductId = 15, ProductName = "OPPO Find N3 Flip (12GB/256GB)", CategoryId = 2, UnitPrice = 1000, Quantity = 20, Image = "image15.jpg" },
                    new Product { ProductId = 16, ProductName = "OPPO Reno10 5G (8GB/256GB)", CategoryId = 2, UnitPrice = 500, Quantity = 20, Image = "image16.jpg" },
                    new Product { ProductId = 17, ProductName = "Xiaomi 13T (12GB/256GB)", CategoryId = 2, UnitPrice = 400, Quantity = 20, Image = "image17.jpg" },
                    new Product { ProductId = 18, ProductName = "Xiaomi 13 Lite (8GB/256GB)", CategoryId = 2, UnitPrice = 300, Quantity = 20, Image = "image18.jpg" },
                    new Product { ProductId = 19, ProductName = "TECNO CAMON 20 Pro (8GB/256GB)", CategoryId = 2, UnitPrice = 200, Quantity = 20, Image = "image19.jpg" },
                    new Product { ProductId = 20, ProductName = "TECNO CAMON 20 (8GB/256GB)", CategoryId = 2, UnitPrice = 300, Quantity = 20, Image = "image20.jpg" },
                    new Product { ProductId = 21, ProductName = "Laptop Dell Vostro 3520-V5I3614W1 (i3-1215U/8GB/256GB/15.6 FHD/Windows 11+ Office)", CategoryId = 3, UnitPrice = 600, Quantity = 20, Image = "image21.jpg" },
                    new Product { ProductId = 22, ProductName = "Laptop Dell Inspiron 16 5625 99VP91 R7-5825U/8GD4/512SSD/16.0FHD+/W11SL+OFFICE ST/BẠC", CategoryId = 3, UnitPrice = 700, Quantity = 20, Image = "image22.jpg" },
                    new Product { ProductId = 23, ProductName = "MacBook Air M2 15 (8GB/256GB)", CategoryId = 3, UnitPrice = 1300, Quantity = 20, Image = "image23.jpg" },
                    new Product { ProductId = 24, ProductName = "MacBook Pro 16 (M2 Pro/12-core CPU/19-core GPU/16GB/512GB)", CategoryId = 3, UnitPrice = 2000, Quantity = 20, Image = "image24.jpg" },
                    new Product { ProductId = 25, ProductName = "Laptop ASUS TUF Gaming F15 FX507ZC4-HN099W (i7-12700H/8GB/512GB/RTX 3050/15.6 FHD 144Hz/Windows 11)", CategoryId = 3, UnitPrice = 1000, Quantity = 20, Image = "image25.jpg" },
                    new Product { ProductId = 26, ProductName = "Laptop ASUS VivoBook Go 14 E1404FA-NK177W (R5-7520U/16GB/512GB/14 FHD/Windows 11)", CategoryId = 3, UnitPrice = 600, Quantity = 20, Image = "image26.jpg" },
                    new Product { ProductId = 27, ProductName = "Laptop Lenovo IdeaPad Slim 3 14IAH8 83EQ0005VN", CategoryId = 3, UnitPrice = 600, Quantity = 20, Image = "image27.jpg" },
                    new Product { ProductId = 28, ProductName = "Laptop Lenovo Thinkbook 16P G2 ACH 20YM003JVN", CategoryId = 3, UnitPrice = 1000, Quantity = 20, Image = "image28.jpg" },
                    new Product { ProductId = 29, ProductName = "Laptop Gaming Acer Nitro 5 Eagle AN515-57-5669 NH.QEHSV.001", CategoryId = 3, UnitPrice = 700, Quantity = 20, Image = "image29.jpg" },
                    new Product { ProductId = 30, ProductName = "Laptop Gaming Acer Nitro 5 Tiger AN515 58 52SP", CategoryId = 3, UnitPrice = 1000, Quantity = 20, Image = "image30.jpg" },
                    new Product { ProductId = 31, ProductName = "Samsung Galaxy Watch5 40mm ", CategoryId = 4, UnitPrice = 100, Quantity = 20, Image = "image31.jpg" },
                    new Product { ProductId = 32, ProductName = "Apple Watch Ultra 2 LTE 49mm", CategoryId = 4, UnitPrice = 1000, Quantity = 20, Image = "image32.jpg" }
                );
            modelBuilder.Entity<User>().HasData(
                    new User { UserId = 1 , Email = "abcd@gmail.com" , Password = "12345678" , UserName ="Nguyen Thi A" , Phone = "0921837232" , Country = "Ha Noi" },
                    new User { UserId = 2, Email = "sdsds1234@gmail.com", Password = "12345678", UserName = "Nguyen Van B", Phone = "0921854321", Country = "Ha Noi" }

                );
            modelBuilder.Entity<Order>().HasData(
                    new Order { OrderId = 1, UserId = 1 , OrderDate = DateTime.Parse("2023/11/01") , RequireDate = DateTime.Parse("2023/11/01"), ShippedDate = DateTime.Parse("2023/11/10")}
                );
            modelBuilder.Entity<OrderDetail>().HasData(
                    new OrderDetail { OrderDetailId = 1 , OrderId = 1 , ProductId = 1 , Quantity = 1 , Discount = 20 , Price = 800 }
                );
        }
    }
}
