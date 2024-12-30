using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProceduresstudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert Student Stored Procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[InsertStudent]
                    @Name NVARCHAR(MAX),
                    @InstituteId INT,
                    @InsertedStudentId INT OUTPUT
                AS
                BEGIN
                    -- Insert a new student
                    INSERT INTO [dbo].[Students] (Name, InstituteId)
                    VALUES (@Name, @InstituteId);
                    
                    -- Return the inserted StudentId using the OUTPUT parameter
                    SET @InsertedStudentId = SCOPE_IDENTITY();
                END
            ");
            // Get All Students Stored Procedure
            migrationBuilder.Sql(@"
              CREATE PROCEDURE [dbo].[GetAllStudents]
               AS
               BEGIN
                    SELECT [StudentId], [Name], [InstituteId]      FROM [dbo].[Students];
                      END
              ");

            // Get Student By Id Stored Procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetStudentById]
                    @StudentId INT
                AS
                BEGIN
                    SELECT [StudentId], [Name], [InstituteId]
                    FROM [dbo].[Students]
                    WHERE [StudentId] = @StudentId;
                END
            ");

            // Update Student Stored Procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[UpdateStudent]
                    @StudentId INT,
                    @Name NVARCHAR(MAX),
                    @InstituteId INT,
                    @UpdatedStudentId INT OUTPUT
                AS
                BEGIN
                    -- Update the student with the given StudentId
                    UPDATE [dbo].[Students]
                    SET [Name] = @Name, [InstituteId] = @InstituteId
                    WHERE [StudentId] = @StudentId;
                    
                    -- If rows are updated, return the StudentId
                    IF @@ROWCOUNT > 0
                    BEGIN
                        SET @UpdatedStudentId = @StudentId;
                    END
                    ELSE
                    BEGIN
                        SET @UpdatedStudentId = NULL; -- No rows updated
                    END
                END
            ");

            // Delete Student Stored Procedure
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[DeleteStudent]
                    @StudentId INT,
                    @DeletedStudentId INT OUTPUT
                AS
                BEGIN
                    -- Delete the student with the given StudentId
                    DELETE FROM [dbo].[Students]
                    WHERE [StudentId] = @StudentId;
                    
                    -- If rows are deleted, return the StudentId
                    IF @@ROWCOUNT > 0
                    BEGIN
                        SET @DeletedStudentId = @StudentId;
                    END
                    ELSE
                    BEGIN
                        SET @DeletedStudentId = NULL; -- No rows deleted
                    END
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the stored procedures in the Down method to reverse the migration
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[InsertStudent]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetStudentById]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[UpdateStudent]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[DeleteStudent]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetAllStudents]");

        }
    }
}
