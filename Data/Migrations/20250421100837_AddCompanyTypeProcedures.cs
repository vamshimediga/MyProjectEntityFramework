using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddCompanyTypeProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllCompanyTypes
                AS
                BEGIN
                    DECLARE @sql NVARCHAR(MAX)
                    SET @sql = N'SELECT * FROM CompanyTypes'
                    EXEC sp_executesql @sql
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetCompanyTypeById
                    @CompanyTypeID INT
                AS
                BEGIN
                    DECLARE @sql NVARCHAR(MAX)
                    SET @sql = N'SELECT * FROM CompanyTypes WHERE CompanyTypeID = @CompanyTypeID'
                    EXEC sp_executesql @sql, N'@CompanyTypeID INT', @CompanyTypeID
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE InsertCompanyType
                    @CompanyTypeName NVARCHAR(MAX)
                AS
                BEGIN
                    DECLARE @sql NVARCHAR(MAX)
                    SET @sql = N'INSERT INTO CompanyTypes (CompanyTypeName) VALUES (@CompanyTypeName)'
                    EXEC sp_executesql @sql, N'@CompanyTypeName NVARCHAR(MAX)', @CompanyTypeName
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateCompanyType
                    @CompanyTypeID INT,
                    @CompanyTypeName NVARCHAR(MAX)
                AS
                BEGIN
                    DECLARE @sql NVARCHAR(MAX)
                    SET @sql = N'UPDATE CompanyTypes SET CompanyTypeName = @CompanyTypeName WHERE CompanyTypeID = @CompanyTypeID'
                    EXEC sp_executesql @sql, 
                        N'@CompanyTypeID INT, @CompanyTypeName NVARCHAR(MAX)', 
                        @CompanyTypeID, @CompanyTypeName
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteCompanyType
                    @CompanyTypeID INT
                AS
                BEGIN
                    DECLARE @sql NVARCHAR(MAX)
                    SET @sql = N'DELETE FROM CompanyTypes WHERE CompanyTypeID = @CompanyTypeID'
                    EXEC sp_executesql @sql, N'@CompanyTypeID INT', @CompanyTypeID
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllCompanyTypes;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetCompanyTypeById;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertCompanyType;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateCompanyType;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteCompanyType;");
        }
    }
}
