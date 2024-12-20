using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Stored Procedure: Get All Customers
            migrationBuilder.Sql(@"
            CREATE PROCEDURE GetAllCustomers
            AS
            BEGIN
                SELECT * FROM Customer
            END
        ");

            // Stored Procedure: Get Customer By ID
            migrationBuilder.Sql(@"
            CREATE PROCEDURE GetCustomerById
                @CustomerId INT
            AS
            BEGIN
                SELECT * FROM Customer WHERE CustomerId = @CustomerId
            END
        ");

            // Stored Procedure: Add Customer
            migrationBuilder.Sql(@"
            CREATE PROCEDURE AddCustomer
                @Name NVARCHAR(100),
                @Email NVARCHAR(100),
                @PhoneNumber NVARCHAR(15)
            AS
            BEGIN
                INSERT INTO Customer (CustomerName, Email, PhoneNumber)
                VALUES (@Name, @Email, @PhoneNumber)
            END
        ");

            // Stored Procedure: Update Customer
            migrationBuilder.Sql(@"
            CREATE PROCEDURE UpdateCustomer
                @CustomerId INT,
                @Name NVARCHAR(100),
                @Email NVARCHAR(100),
                @PhoneNumber NVARCHAR(15)
            AS
            BEGIN
                UPDATE Customer
                SET CustomerName = @Name,
                    Email = @Email,
                    PhoneNumber = @PhoneNumber
                WHERE CustomerId = @CustomerId
            END
        ");

            // Stored Procedure: Delete Customer
            migrationBuilder.Sql(@"
            CREATE PROCEDURE DeleteCustomer
                @CustomerId INT
            AS
            BEGIN
                DELETE FROM Customer WHERE CustomerId = @CustomerId
            END
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop Stored Procedures
            migrationBuilder.Sql("DROP PROCEDURE GetAllCustomers");
            migrationBuilder.Sql("DROP PROCEDURE GetCustomerById");
            migrationBuilder.Sql("DROP PROCEDURE AddCustomer");
            migrationBuilder.Sql("DROP PROCEDURE UpdateCustomer");
            migrationBuilder.Sql("DROP PROCEDURE DeleteCustomer");
        }
    }
}
