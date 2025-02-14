using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class laywerclientsp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Stored Procedure to Get All Lawyers
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllLawyers
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT * FROM Lawyers;
                END;
            ");

            // Stored Procedure to Get Lawyer by ID
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetLawyerById
                    @LawyerID INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT * FROM Lawyers WHERE LawyerID = @LawyerID;
                END;
            ");

            // Stored Procedure to Insert Lawyer
            migrationBuilder.Sql(@"
                CREATE PROCEDURE InsertLawyer
                    @Name NVARCHAR(MAX),
                    @Specialization NVARCHAR(MAX),
                    @Experience INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO Lawyers (Name, Specialization, Experience) 
                    VALUES (@Name, @Specialization, @Experience);
                END;
            ");

            // Stored Procedure to Update Lawyer
            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateLawyer
                    @LawyerID INT,
                    @Name NVARCHAR(MAX),
                    @Specialization NVARCHAR(MAX),
                    @Experience INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    UPDATE Lawyers 
                    SET Name = @Name, Specialization = @Specialization, Experience = @Experience
                    WHERE LawyerID = @LawyerID;
                END;
            ");

            // Stored Procedure to Delete Lawyer
            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteLawyer
                    @LawyerID INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DELETE FROM Lawyers WHERE LawyerID = @LawyerID;
                END;
            ");

            // Stored Procedure to Get All Clients
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllClients
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT * FROM Clients;
                END;
            ");

            // Stored Procedure to Get Client by ID
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetClientById
                    @ClientID INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT * FROM Clients WHERE ClientID = @ClientID;
                END;
            ");

            // Stored Procedure to Insert Client
            migrationBuilder.Sql(@"
                CREATE PROCEDURE InsertClient
                    @LawyerID INT,
                    @Name NVARCHAR(MAX),
                    @CaseType NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO Clients (LawyerID, Name, CaseType) 
                    VALUES (@LawyerID, @Name, @CaseType);
                END;
            ");

            // Stored Procedure to Update Client
            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateClient
                    @ClientID INT,
                    @LawyerID INT,
                    @Name NVARCHAR(MAX),
                    @CaseType NVARCHAR(MAX)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    UPDATE Clients 
                    SET LawyerID = @LawyerID, Name = @Name, CaseType = @CaseType
                    WHERE ClientID = @ClientID;
                END;
            ");

            // Stored Procedure to Delete Client
            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteClient
                    @ClientID INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DELETE FROM Clients WHERE ClientID = @ClientID;
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop Stored Procedures if Migration is Rolled Back
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllLawyers;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetLawyerById;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertLawyer;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateLawyer;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteLawyer;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllClients;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetClientById;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertClient;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateClient;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteClient;");
        }
    }
}
