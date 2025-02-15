using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class spfor_Department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Stored Procedure: Get All Employees
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllEmployees
                AS
                BEGIN
                    SELECT 
                        EmployeeID, 
                        EmployeeName, 
                        DepartmentID
                    FROM 
                        Employees;
                END;
            ");

            // Stored Procedure: Get Employee By ID
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetEmployeeById
                    @EmployeeID INT
                AS
                BEGIN
                    SELECT 
                        EmployeeID, 
                        EmployeeName, 
                        DepartmentID
                    FROM 
                        Employees
                    WHERE 
                        EmployeeID = @EmployeeID;
                END;
            ");

            // Stored Procedure: Insert Employee
            migrationBuilder.Sql(@"
                CREATE PROCEDURE InsertEmployee
                    @EmployeeName NVARCHAR(MAX),
                    @DepartmentID INT,
                    @NewEmployeeID INT OUTPUT
                AS
                BEGIN
                    INSERT INTO Employees (EmployeeName, DepartmentID)
                    VALUES (@EmployeeName, @DepartmentID);

                    SET @NewEmployeeID = SCOPE_IDENTITY();
                END;
            ");

            // Stored Procedure: Update Employee
            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateEmployee
                    @EmployeeID INT,
                    @EmployeeName NVARCHAR(MAX),
                    @DepartmentID INT,
                    @IsUpdated BIT OUTPUT
                AS
                BEGIN
                    IF EXISTS (SELECT 1 FROM Employees WHERE EmployeeID = @EmployeeID)
                    BEGIN
                        UPDATE Employees
                        SET 
                            EmployeeName = @EmployeeName,
                            DepartmentID = @DepartmentID
                        WHERE 
                            EmployeeID = @EmployeeID;

                        SET @IsUpdated = 1;
                    END
                    ELSE
                    BEGIN
                        SET @IsUpdated = 0;
                    END;
                END;
            ");

            // Stored Procedure: Delete Employee
            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteEmployee
                    @EmployeeID INT,
                    @IsDeleted BIT OUTPUT
                AS
                BEGIN
                    IF EXISTS (SELECT 1 FROM Employees WHERE EmployeeID = @EmployeeID)
                    BEGIN
                        DELETE FROM Employees
                        WHERE EmployeeID = @EmployeeID;

                        SET @IsDeleted = 1;
                    END
                    ELSE
                    BEGIN
                        SET @IsDeleted = 0;
                    END;
                END;
            ");

            // Stored Procedure: Get All Departments
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetAllDepartments
                AS
                BEGIN
                    SELECT 
                        DepartmentID, 
                        DepartmentName
                    FROM 
                        Departments;
                END;
            ");

            // Stored Procedure: Get Department By ID
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetDepartmentById
                    @DepartmentID INT
                AS
                BEGIN
                    SELECT 
                        DepartmentID, 
                        DepartmentName
                    FROM 
                        Departments
                    WHERE 
                        DepartmentID = @DepartmentID;
                END;
            ");

            // Stored Procedure: Insert Department
            migrationBuilder.Sql(@"
                CREATE PROCEDURE InsertDepartment
                    @DepartmentName NVARCHAR(MAX),
                    @NewDepartmentID INT OUTPUT
                AS
                BEGIN
                    INSERT INTO Departments (DepartmentName)
                    VALUES (@DepartmentName);

                    SET @NewDepartmentID = SCOPE_IDENTITY();
                END;
            ");

            // Stored Procedure: Update Department
            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateDepartment
                    @DepartmentID INT,
                    @DepartmentName NVARCHAR(MAX),
                    @IsUpdated BIT OUTPUT
                AS
                BEGIN
                    IF EXISTS (SELECT 1 FROM Departments WHERE DepartmentID = @DepartmentID)
                    BEGIN
                        UPDATE Departments
                        SET 
                            DepartmentName = @DepartmentName
                        WHERE 
                            DepartmentID = @DepartmentID;

                        SET @IsUpdated = 1;
                    END
                    ELSE
                    BEGIN
                        SET @IsUpdated = 0;
                    END;
                END;
            ");

            // Stored Procedure: Delete Department
            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteDepartment
                    @DepartmentID INT,
                    @IsDeleted BIT OUTPUT
                AS
                BEGIN
                    IF EXISTS (SELECT 1 FROM Departments WHERE DepartmentID = @DepartmentID)
                    BEGIN
                        DELETE FROM Departments
                        WHERE DepartmentID = @DepartmentID;

                        SET @IsDeleted = 1;
                    END
                    ELSE
                    BEGIN
                        SET @IsDeleted = 0;
                    END;
                END;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop Stored Procedures
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllEmployees");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetEmployeeById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertEmployee");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateEmployee");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteEmployee");

            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllDepartments");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetDepartmentById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertDepartment");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateDepartment");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteDepartment");
        }
    }
}
