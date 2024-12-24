using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddedStore_procedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Stored Procedure: Get All Products
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllProducts
                AS
                BEGIN
                    SELECT * FROM Products
                END
            ");

            // Stored Procedure: Get Product By ID
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetProductById
                    @ProductId INT
                AS
                BEGIN
                    SELECT * FROM Products WHERE ProductId = @ProductId
                END
            ");

            // Stored Procedure: Add Product
            migrationBuilder.Sql(@"
                CREATE PROCEDURE AddProduct
                    @ProductName NVARCHAR(100),
                    @Price DECIMAL(18, 2),
                    @CategoryId INT
                AS
                BEGIN
                    INSERT INTO Products (ProductName, Price, CategoryId)
                    VALUES (@ProductName, @Price, @CategoryId);
                END
            ");

            // Stored Procedure: Update Product
            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateProduct
                    @ProductId INT,
                    @ProductName NVARCHAR(100),
                    @Price DECIMAL(18, 2),
                    @CategoryId INT
                AS
                BEGIN
                    UPDATE Products
                    SET ProductName = @ProductName,
                        Price = @Price,
                        CategoryId = @CategoryId
                    WHERE ProductId = @ProductId;
                END
            ");

            // Stored Procedure: Delete Product
            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteProduct
                    @ProductId INT
                AS
                BEGIN
                    DELETE FROM Products WHERE ProductId = @ProductId;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the stored procedures if the migration is rolled back
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllProducts");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetProductById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AddProduct");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateProduct");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteProduct");
        }
    }
}
