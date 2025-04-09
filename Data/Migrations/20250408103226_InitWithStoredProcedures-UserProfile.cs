using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitWithStoredProceduresUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Get All
            migrationBuilder.Sql(@"
    CREATE PROCEDURE GetAllUserProfiles
    AS
    BEGIN
        SELECT * FROM UserProfiles
    END
");

            // 2. Get By ID
            migrationBuilder.Sql(@"
    CREATE PROCEDURE GetByIdUserProfiles
        @UserProfileId INT
    AS
    BEGIN
        SELECT * FROM UserProfiles WHERE UserProfileId = @UserProfileId
    END
");

            // 3. Insert
            migrationBuilder.Sql(@"
    CREATE PROCEDURE InsertUserProfile
        @FullName NVARCHAR(100),
        @Age INT,
        @Gender NVARCHAR(10)
    AS
    BEGIN
        INSERT INTO UserProfiles (FullName, Age, Gender)
        VALUES (@FullName, @Age, @Gender)
    END
");

            // 4. Update
            migrationBuilder.Sql(@"
    CREATE PROCEDURE UpdateUserProfile
        @UserProfileId INT,
        @FullName NVARCHAR(100),
        @Age INT,
        @Gender NVARCHAR(10)
    AS
    BEGIN
        UPDATE UserProfiles
        SET FullName = @FullName, Age = @Age, Gender = @Gender
        WHERE UserProfileId = @UserProfileId
    END
");

            // 5. Delete
            migrationBuilder.Sql(@"
    CREATE PROCEDURE DeleteUserProfile
        @UserProfileId INT
    AS
    BEGIN
        DELETE FROM UserProfiles
        WHERE UserProfileId = @UserProfileId
    END
");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllUserProfiles");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetUserProfileById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertUserProfile");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateUserProfile");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteUserProfile");

        }
    }
}
