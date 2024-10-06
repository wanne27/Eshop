using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductServiceApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: ["Name", "Price", "Description", "CreatedDate"],
                values: new object[,]
                {
                    { "Mountain Bike", 500.00m, "A durable mountain bike for rough terrains", DateTime.UtcNow },
                    { "Road Bike", 750.00m, "A fast road bike designed for paved surfaces", DateTime.UtcNow },
                    { "Electric Scooter", 300.00m, "A lightweight electric scooter for city commutes", DateTime.UtcNow },
                    { "Helmet", 50.00m, "A safety helmet for cyclists and bikers", DateTime.UtcNow },
                    { "Gloves", 25.00m, "Cycling gloves for better grip and comfort", DateTime.UtcNow },
                    { "Water Bottle", 15.00m, "A lightweight water bottle for hydration during rides", DateTime.UtcNow },
                    { "Cycling Shorts", 60.00m, "Comfortable cycling shorts with padding", DateTime.UtcNow },
                    { "Bike Lock", 40.00m, "A durable bike lock to prevent theft", DateTime.UtcNow },
                    { "Handlebar Bag", 35.00m, "A compact handlebar bag for storing essentials", DateTime.UtcNow },
                    { "Repair Kit", 20.00m, "A portable repair kit for quick fixes on the go", DateTime.UtcNow }
                });

            // Хранимая процедура для добавления продукта
            migrationBuilder.Sql(@"
            CREATE PROCEDURE AddProduct
                @Name NVARCHAR(MAX),
                @Price DECIMAL(18, 2),
                @Description NVARCHAR(MAX),
                @CreatedDate DATETIME2
            AS
            BEGIN
                INSERT INTO Products (Name, Price, Description, CreatedDate)
                VALUES (@Name, @Price, @Description, @CreatedDate);
            END");

            // Хранимая процедура для обновления продукта
            migrationBuilder.Sql(@"
            CREATE PROCEDURE UpdateProduct
                @ProductId INT,
                @Name NVARCHAR(MAX),
                @Price DECIMAL(18, 2),
                @Description NVARCHAR(MAX)
            AS
            BEGIN
                UPDATE Products
                SET Name = @Name, Price = @Price, Description = @Description
                WHERE Id = @ProductId;
            END");

            // Хранимая процедура для удаления продукта
            migrationBuilder.Sql(@"
            CREATE PROCEDURE DeleteProduct
                @ProductId INT
            AS
            BEGIN
                DELETE FROM Products WHERE Id = @ProductId;
            END");

            // Хранимая процедура для обновления цены продукта
            migrationBuilder.Sql(@"
            CREATE PROCEDURE UpdateProductPrice
                @ProductId INT,
                @NewPrice DECIMAL(18, 2)
            AS
            BEGIN
                UPDATE Products SET Price = @NewPrice WHERE Id = @ProductId;
            END");

            // Создание индекса на столбце Price в таблице Products
            migrationBuilder.CreateIndex(
                name: "IX_Products_Price",
                table: "Products",
                column: "Price");

            // Создание триггера для обновления цены в таблице OrderItems
            migrationBuilder.Sql(@"
            CREATE TRIGGER trg_UpdateOrderItemPrice
            ON Products
            AFTER UPDATE
            AS
            BEGIN
                -- Обновление цены в таблице OrderItems
                UPDATE oi
                SET oi.UnitPrice = i.Price
                FROM [OrderServiceDb].[dbo].[OrderItems] oi
                INNER JOIN inserted i ON oi.ProductId = i.Id
                WHERE oi.ProductId IN (SELECT Id FROM inserted);

                -- Обновление общей суммы в таблице Orders
                UPDATE o
                SET o.TotalAmount = (SELECT SUM(oi.UnitPrice * oi.Quantity) 
                                     FROM [OrderServiceDb].[dbo].[OrderItems] oi 
                                     WHERE oi.OrderId = o.Id)
                FROM [OrderServiceDb].[dbo].[Orders] o
                WHERE o.Id IN (SELECT DISTINCT oi.OrderId 
                                FROM [OrderServiceDb].[dbo].[OrderItems] oi 
                                INNER JOIN inserted i ON oi.ProductId = i.Id);
            END;
            ");
        }       

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AddProduct");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateProduct");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteProduct");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateProductPrice");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_UpdateOrderItemPrice;");
            migrationBuilder.DropIndex(
                name: "IX_Products_Price",
                table: "Products");
        }
    }
}
