using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class spforcontact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Contacts_Insert Stored Procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[Contacts_Insert]
                    @ContactName NVARCHAR(MAX),
                    @ContactPhone NVARCHAR(MAX),
                    @LeadID INT,
                    @Result BIT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO [dbo].[Contacts] (ContactName, ContactPhone, LeadID)
                    VALUES (@ContactName, @ContactPhone, @LeadID);
                    SET @Result = 1; -- Success
                END
            ");

            // Create Contacts_Update Stored Procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[Contacts_Update]
                    @ContactID INT,
                    @ContactName NVARCHAR(MAX),
                    @ContactPhone NVARCHAR(MAX),
                    @LeadID INT,
                    @Result BIT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    UPDATE [dbo].[Contacts]
                    SET ContactName = @ContactName,
                        ContactPhone = @ContactPhone,
                        LeadID = @LeadID
                    WHERE ContactID = @ContactID;

                    IF @@ROWCOUNT > 0
                        SET @Result = 1; -- Success
                    ELSE
                        SET @Result = 0; -- Failure (no rows affected)
                END
            ");

            // Create Contacts_Delete Stored Procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[Contacts_Delete]
                    @ContactID INT,
                    @Result BIT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DELETE FROM [dbo].[Contacts]
                    WHERE ContactID = @ContactID;

                    IF @@ROWCOUNT > 0
                        SET @Result = 1; -- Success
                    ELSE
                        SET @Result = 0; -- Failure (no rows affected)
                END
            ");

            // Create Contacts_GetById Stored Procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[Contacts_GetById]
                    @ContactID INT,
                    @Result BIT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT ContactID, ContactName, ContactPhone, LeadID
                    FROM [dbo].[Contacts]
                    WHERE ContactID = @ContactID;
                    SET @Result = 1; -- Success
                END
            ");

            // Create Contacts_Get Stored Procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[Contacts_Get]
                    @Result BIT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT ContactID, ContactName, ContactPhone, LeadID
                    FROM [dbo].[Contacts];
                    SET @Result = 1; -- Success
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop Contacts_Insert Stored Procedure
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Contacts_Insert]");

            // Drop Contacts_Update Stored Procedure
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Contacts_Update]");

            // Drop Contacts_Delete Stored Procedure
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Contacts_Delete]");

            // Drop Contacts_GetById Stored Procedure
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Contacts_GetById]");

            // Drop Contacts_Get Stored Procedure
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Contacts_Get]");
        }
    }
}
