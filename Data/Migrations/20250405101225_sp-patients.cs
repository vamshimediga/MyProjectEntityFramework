using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class sppatients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Your existing column modifications remain here...

            // 1. Get All Patients
            migrationBuilder.Sql(@"
        CREATE PROCEDURE GetAllPatients
        AS
        BEGIN
            SELECT * FROM Patients;
        END
    ");

            // 2. Get Patient By ID
            migrationBuilder.Sql(@"
        CREATE PROCEDURE GetPatientById
            @PatientID INT
        AS
        BEGIN
            SELECT * FROM Patients WHERE PatientID = @PatientID;
        END
    ");

            // 3. Insert Patient
            migrationBuilder.Sql(@"
        CREATE PROCEDURE InsertPatient
            @FirstName NVARCHAR(MAX),
            @LastName NVARCHAR(MAX),
            @ContactNumber NVARCHAR(MAX)
        AS
        BEGIN
            INSERT INTO Patients (FirstName, LastName, ContactNumber)
            VALUES (@FirstName, @LastName, @ContactNumber);
        END
    ");

            // 4. Update Patient
            migrationBuilder.Sql(@"
        CREATE PROCEDURE UpdatePatient
            @PatientID INT,
            @FirstName NVARCHAR(MAX),
            @LastName NVARCHAR(MAX),
            @ContactNumber NVARCHAR(MAX)
        AS
        BEGIN
            UPDATE Patients
            SET FirstName = @FirstName,
                LastName = @LastName,
                ContactNumber = @ContactNumber
            WHERE PatientID = @PatientID;
        END
    ");

            // 5. Delete Patient
            migrationBuilder.Sql(@"
        CREATE PROCEDURE DeletePatient
            @PatientID INT
        AS
        BEGIN
            DELETE FROM Patients WHERE PatientID = @PatientID;
        END
    ");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Your existing Down code for reverting columns...

            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllPatients");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetPatientById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS InsertPatient");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdatePatient");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeletePatient");
        }

    }
}
