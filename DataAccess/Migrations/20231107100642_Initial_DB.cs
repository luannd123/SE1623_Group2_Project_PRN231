using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Initial_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Television" },
                    { 2, "Telephone" },
                    { 3, "Laptop" },
                    { 4, "Smart Watch" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Country", "Email", "Password", "Phone", "UserName" },
                values: new object[,]
                {
                    { 1, "Ha Noi", "abcd@gmail.com", "12345678", "0921837232", "Nguyen Thi A" },
                    { 2, "Ha Noi", "sdsds1234@gmail.com", "12345678", "0921854321", "Nguyen Van B" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "OrderDate", "RequireDate", "ShippedDate", "UserId" },
                values: new object[] { 1, new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Image", "ProductName", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, "image01.jpg", "TV SAMSUNG VOLET 50' 4K", 20, 1000m },
                    { 2, 1, "image02.jpg", "TV SAMSUNG VOLET 45' 4k", 20, 500m },
                    { 3, 1, "image03.jpg", "TV SAMSUNG VOLET 50' 2K", 20, 400m },
                    { 4, 1, "image04.jpg", "TV SONY Bravia 50' 4K", 20, 1000m },
                    { 5, 1, "image05.jpg", "TV SAMSUNG QOLET 50' 4K", 20, 1000m },
                    { 6, 1, "image06.jpg", "TV SAMSUNG QOLET 50' 2K", 20, 1000m },
                    { 7, 1, "image07.jpg", "Google TV Sony 4K 43 inch KD-43X77L", 20, 1000m },
                    { 8, 1, "image08.jpg", "Smart TV NanoCell LG 4K 65 inch 65NANO76SQA", 20, 1000m },
                    { 9, 1, "image09.jpg", "Smart TV LG 4K 55 inch 55UQ8000PSC", 20, 1000m },
                    { 10, 1, "image10.jpg", "Smart TV OLED LG 4K 55 inch 55C2PSA", 20, 1000m },
                    { 11, 2, "image11.jpg", "iPhone 15 Pro Max (512GB)", 20, 1500m },
                    { 12, 2, "image12.jpg", "iPhone 15 Plus (128GB)", 20, 1000m },
                    { 13, 2, "image13.jpg", "Samsung Galaxy Z Fold5 12GB/256GB", 20, 1000m },
                    { 14, 2, "image14.jpg", "Samsung Galaxy Z Flip5 8GB/256GB ", 20, 800m },
                    { 15, 2, "image15.jpg", "OPPO Find N3 Flip (12GB/256GB)", 20, 1000m },
                    { 16, 2, "image16.jpg", "OPPO Reno10 5G (8GB/256GB)", 20, 500m },
                    { 17, 2, "image17.jpg", "Xiaomi 13T (12GB/256GB)", 20, 400m },
                    { 18, 2, "image18.jpg", "Xiaomi 13 Lite (8GB/256GB)", 20, 300m },
                    { 19, 2, "image19.jpg", "TECNO CAMON 20 Pro (8GB/256GB)", 20, 200m },
                    { 20, 2, "image20.jpg", "TECNO CAMON 20 (8GB/256GB)", 20, 300m },
                    { 21, 3, "image21.jpg", "Laptop Dell Vostro 3520-V5I3614W1 (i3-1215U/8GB/256GB/15.6 FHD/Windows 11+ Office)", 20, 600m },
                    { 22, 3, "image22.jpg", "Laptop Dell Inspiron 16 5625 99VP91 R7-5825U/8GD4/512SSD/16.0FHD+/W11SL+OFFICE ST/BẠC", 20, 700m },
                    { 23, 3, "image23.jpg", "MacBook Air M2 15 (8GB/256GB)", 20, 1300m },
                    { 24, 3, "image24.jpg", "MacBook Pro 16 (M2 Pro/12-core CPU/19-core GPU/16GB/512GB)", 20, 2000m },
                    { 25, 3, "image25.jpg", "Laptop ASUS TUF Gaming F15 FX507ZC4-HN099W (i7-12700H/8GB/512GB/RTX 3050/15.6 FHD 144Hz/Windows 11)", 20, 1000m },
                    { 26, 3, "image26.jpg", "Laptop ASUS VivoBook Go 14 E1404FA-NK177W (R5-7520U/16GB/512GB/14 FHD/Windows 11)", 20, 600m },
                    { 27, 3, "image27.jpg", "Laptop Lenovo IdeaPad Slim 3 14IAH8 83EQ0005VN", 20, 600m },
                    { 28, 3, "image28.jpg", "Laptop Lenovo Thinkbook 16P G2 ACH 20YM003JVN", 20, 1000m },
                    { 29, 3, "image29.jpg", "Laptop Gaming Acer Nitro 5 Eagle AN515-57-5669 NH.QEHSV.001", 20, 700m },
                    { 30, 3, "image30.jpg", "Laptop Gaming Acer Nitro 5 Tiger AN515 58 52SP", 20, 1000m },
                    { 31, 4, "image31.jpg", "Samsung Galaxy Watch5 40mm ", 20, 100m },
                    { 32, 4, "image32.jpg", "Apple Watch Ultra 2 LTE 49mm", 20, 1000m }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "Discount", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[] { 1, 20, 1, 800m, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
