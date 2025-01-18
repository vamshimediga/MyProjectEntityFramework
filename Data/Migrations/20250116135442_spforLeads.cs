using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class spforLeads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Creating the stored procedure for Insert operation in Leads
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[Leads_Insert]
                    @FirstName NVARCHAR(MAX),
                    @LastName NVARCHAR(MAX),
                    @Email NVARCHAR(MAX),
                    @Result BIT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    INSERT INTO [dbo].[Leads] (FirstName, LastName, Email)
                    VALUES (@FirstName, @LastName, @Email);

                    SET @Result = 1; -- Success
                END
            ");

            // Creating the stored procedure for Update operation in Leads
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[Leads_Update]
                    @LeadID INT,
                    @FirstName NVARCHAR(MAX),
                    @LastName NVARCHAR(MAX),
                    @Email NVARCHAR(MAX),
                    @Result BIT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    UPDATE [dbo].[Leads]
                    SET FirstName = @FirstName,
                        LastName = @LastName,
                        Email = @Email
                    WHERE LeadID = @LeadID;

                    IF @@ROWCOUNT > 0
                        SET @Result = 1; -- Success
                    ELSE
                        SET @Result = 0; -- Failure (no rows affected)
                END
            ");

            // Creating the stored procedure for Delete operation in Leads
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[Leads_Delete]
                    @LeadID INT,
                    @Result BIT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    DELETE FROM [dbo].[Leads]
                    WHERE LeadID = @LeadID;

                    IF @@ROWCOUNT > 0
                        SET @Result = 1; -- Success
                    ELSE
                        SET @Result = 0; -- Failure (no rows affected)
                END
            ");

            // Creating the stored procedure for GetById operation in Leads
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[Leads_GetById]
                    @LeadID INT,
                
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT LeadID, FirstName, LastName, Email
                    FROM [dbo].[Leads]
                    WHERE LeadID = @LeadID;

                   
                END
            ");

            // Creating the stored procedure for Get operation in Leads
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[Leads_GetAll]
                   
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT LeadID, FirstName, LastName, Email
                    FROM [dbo].[Leads];

               
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Dropping the stored procedures
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Leads_Insert]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Leads_Update]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Leads_Delete]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Leads_GetById]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Leads_GetAll]");
        }
    }
}
