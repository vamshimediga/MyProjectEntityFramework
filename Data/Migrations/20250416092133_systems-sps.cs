using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class systemssps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Get All
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetAllSystemAdmins]
                AS
                BEGIN
                    SELECT 
                        SystemAdminId,
                        AdminName,
                        DeveloperId
                    FROM [dbo].[SystemAdmins]
                END
            ");

            // Get By ID
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetSystemAdminById]
                    @SystemAdminId INT
                AS
                BEGIN
                    SELECT 
                        SystemAdminId,
                        AdminName,
                        DeveloperId
                    FROM [dbo].[SystemAdmins]
                    WHERE SystemAdminId = @SystemAdminId
                END
            ");

            // Insert
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[InsertSystemAdmin]
                    @AdminName NVARCHAR(MAX),
                    @DeveloperId INT
                AS
                BEGIN
                    INSERT INTO [dbo].[SystemAdmins] (AdminName, DeveloperId)
                    VALUES (@AdminName, @DeveloperId)

                    SELECT SCOPE_IDENTITY() AS NewSystemAdminId
                END
            ");

            // Update
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[UpdateSystemAdmin]
                    @SystemAdminId INT,
                    @AdminName NVARCHAR(MAX),
                    @DeveloperId INT
                AS
                BEGIN
                    UPDATE [dbo].[SystemAdmins]
                    SET AdminName = @AdminName,
                        DeveloperId = @DeveloperId
                    WHERE SystemAdminId = @SystemAdminId
                END
            ");

            // Delete
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[DeleteSystemAdmin]
                    @SystemAdminId INT
                AS
                BEGIN
                    DELETE FROM [dbo].[SystemAdmins]
                    WHERE SystemAdminId = @SystemAdminId
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetAllSystemAdmins]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetSystemAdminById]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[InsertSystemAdmin]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[UpdateSystemAdmin]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[DeleteSystemAdmin]");
        }
    }
}
