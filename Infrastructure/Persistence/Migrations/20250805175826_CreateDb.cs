using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Details_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Created", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 5, 21, 58, 25, 512, DateTimeKind.Local).AddTicks(9815), false, "HP" },
                    { 2, new DateTime(2025, 8, 5, 21, 58, 25, 517, DateTimeKind.Local).AddTicks(7612), false, "Dilvin" },
                    { 3, new DateTime(2025, 8, 5, 21, 58, 25, 517, DateTimeKind.Local).AddTicks(7657), false, "DELL" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "IsDeleted", "Name", "ParentId", "Priority" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 5, 21, 58, 25, 520, DateTimeKind.Local).AddTicks(3791), false, "Elektrik", 0, 1 },
                    { 2, new DateTime(2025, 8, 5, 21, 58, 25, 520, DateTimeKind.Local).AddTicks(3817), false, "Moda", 0, 2 },
                    { 3, new DateTime(2025, 8, 5, 21, 58, 25, 520, DateTimeKind.Local).AddTicks(3820), false, "Bilgisayar", 1, 1 },
                    { 4, new DateTime(2025, 8, 5, 21, 58, 25, 520, DateTimeKind.Local).AddTicks(3823), false, "Kadın", 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "Id", "CategoryId", "Created", "Description", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 8, 5, 21, 58, 25, 521, DateTimeKind.Local).AddTicks(6619), "Cihazın ne kadar süre çalışabileceğini belirler. mAh (miliamper-saat) cinsinden ölçülür. Daha yüksek değer, daha uzun kullanım süresi sağlar.", false, "Pil Kapasitesi" },
                    { 2, 1, new DateTime(2025, 8, 5, 21, 58, 25, 521, DateTimeKind.Local).AddTicks(6652), "Cihazın ekran tipi ve çözünürlüğü. Örn: AMOLED, LCD, 1080x2400 piksel. Görüntü kalitesi ve kullanıcı deneyimini etkiler.", false, "Ekran Özelliği" },
                    { 3, 1, new DateTime(2025, 8, 5, 21, 58, 25, 521, DateTimeKind.Local).AddTicks(6655), "Cihazın çalışma sırasında harcadığı elektrik miktarıdır. Watt (W) cinsinden ölçülür. Enerji verimliliği açısından önemlidir.", false, "Enerji Tüketimi" },
                    { 4, 2, new DateTime(2025, 8, 5, 21, 58, 25, 521, DateTimeKind.Local).AddTicks(6657), "Bilgisayarın temel işlem gücünü belirler. Program derleme, uygulama çalıştırma gibi işlemlerde performansı etkiler. Örn: Intel Core i7, AMD Ryzen 5.", false, "İşlemci (CPU)" },
                    { 5, 2, new DateTime(2025, 8, 5, 21, 58, 25, 521, DateTimeKind.Local).AddTicks(6660), "Aynı anda çalıştırılan uygulamaların hızlı çalışmasını sağlar. 8 GB ve üzeri RAM, yazılım geliştirme için idealdir.", false, "RAM (Bellek)" },
                    { 6, 2, new DateTime(2025, 8, 5, 21, 58, 25, 521, DateTimeKind.Local).AddTicks(6663), "Grafik işlem görevlerini yerine getirir. Oyun, video işleme ve bazı yapay zeka uygulamalarında kullanılır. Geliştiriciler için genellikle entegre GPU yeterlidir.", false, "Ekran Kartı (GPU)" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "Created", "Description", "Discount", "IsDeleted", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2025, 8, 5, 21, 58, 25, 523, DateTimeKind.Local).AddTicks(3518), "Yüksek performanslı Intel Core i7 işlemci, 16 GB RAM ve 512 GB SSD ile yazılım geliştirme ve profesyonel kullanım için ideal bir ultrabook.", 2m, false, 2000m, "Dell XPS 15" },
                    { 2, 1, new DateTime(2025, 8, 5, 21, 58, 25, 523, DateTimeKind.Local).AddTicks(3636), "HP VP2 işlemcisi ile hızlı ve verimli çalışma sunar. Retina ekranı ve uzun pil ömrüyle geliştiriciler için tasarlandı.", 1.5m, false, 1500m, "HP  1467 VP2" },
                    { 3, 1, new DateTime(2025, 8, 5, 21, 58, 25, 523, DateTimeKind.Local).AddTicks(3640), "Aktif gürültü engelleme özelliği ve yüksek ses kalitesiyle müzik keyfini zirveye taşıyan kablosuz kulaklık.", 3m, false, 400m, "HP  1467 VP2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Details_CategoryId",
                table: "Details",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
