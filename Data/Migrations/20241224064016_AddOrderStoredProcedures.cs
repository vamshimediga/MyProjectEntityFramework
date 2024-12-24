using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Stored Procedure: Get All Orders
            migrationBuilder.Sql(@"
            CREATE PROCEDURE GetAllOrders
            AS
            BEGIN
                SELECT * FROM [dbo].[Order]
            END
            ");

            // Stored Procedure: Get Order By ID
            migrationBuilder.Sql(@"
            CREATE PROCEDURE GetOrderById
                @OrderId INT
            AS
            BEGIN
                SELECT * FROM [dbo].[Order] WHERE OrderId = @OrderId
            END
            ");

            // Stored Procedure: Add Order
            migrationBuilder.Sql(@"
            CREATE PROCEDURE AddOrder
                @OrderDate DATETIME,
                @TotalAmount DECIMAL(18, 2),
                @OrderStatus NVARCHAR(50),
                @CustomerId INT
            AS
            BEGIN
                INSERT INTO [dbo].[Order] (OrderDate, TotalAmount, OrderStatus, CustomerId)
                VALUES (@OrderDate, @TotalAmount, @OrderStatus, @CustomerId);
            END
            ");

            // Stored Procedure: Update Order
            migrationBuilder.Sql(@"
            CREATE PROCEDURE UpdateOrder
                @OrderId INT,
                @OrderDate DATETIME,
                @TotalAmount DECIMAL(18, 2),
                @OrderStatus NVARCHAR(50),
                @CustomerId INT
            AS
            BEGIN
                UPDATE [dbo].[Order]
                SET OrderDate = @OrderDate,
                    TotalAmount = @TotalAmount,
                    OrderStatus = @OrderStatus,
                    CustomerId = @CustomerId
                WHERE OrderId = @OrderId;
            END
            ");

            // Stored Procedure: Delete Order
            migrationBuilder.Sql(@"
            CREATE PROCEDURE DeleteOrder
                @OrderId INT
            AS
            BEGIN
                DELETE FROM [dbo].[Order] WHERE OrderId = @OrderId;
            END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop Stored Procedures
            migrationBuilder.Sql("DROP PROCEDURE GetAllOrders");
            migrationBuilder.Sql("DROP PROCEDURE GetOrderById");
            migrationBuilder.Sql("DROP PROCEDURE AddOrder");
            migrationBuilder.Sql("DROP PROCEDURE UpdateOrder");
            migrationBuilder.Sql("DROP PROCEDURE DeleteOrder");
        }
    }
}
