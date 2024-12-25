using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddedStore_procedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Stored Procedure: Get All Products
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetAllProducts')
                BEGIN
                    CREATE PROCEDURE GetAllProducts
                    AS
                    BEGIN
                        SELECT * FROM Products
                    END
                END
            ");

            // Stored Procedure: Get Product By ID
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProductById')
                BEGIN
                    CREATE PROCEDURE GetProductById
                        @ProductId INT
                    AS
                    BEGIN
                        SELECT * FROM Products WHERE ProductId = @ProductId
                    END
                END
            ");

            // Stored Procedure: Add Product
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'AddProduct')
                BEGIN
                    CREATE PROCEDURE AddProduct
                        @ProductName NVARCHAR(100),
                        @Price DECIMAL(18, 2),
                        @CategoryId INT
                    AS
                    BEGIN
                        INSERT INTO Products (ProductName, Price, CategoryId)
                        VALUES (@ProductName, @Price, @CategoryId);
                    END
                END
            ");

            // Stored Procedure: Update Product
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UpdateProduct')
                BEGIN
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
                END
            ");

            // Stored Procedure: Delete Product
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'DeleteProduct')
                BEGIN
                    CREATE PROCEDURE DeleteProduct
                        @ProductId INT
                    AS
                    BEGIN
                        DELETE FROM Products WHERE ProductId = @ProductId;
                    END
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
