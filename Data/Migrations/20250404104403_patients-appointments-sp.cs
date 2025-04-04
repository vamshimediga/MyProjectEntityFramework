using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class patientsappointmentssp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Stored Procedure: Get All Appointments
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_GetAllAppointments
                AS
                BEGIN
                    SELECT * FROM Appointments;
                END;
            ");

            // Stored Procedure: Get Appointment by ID
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_GetAppointmentById
                    @AppointmentID INT
                AS
                BEGIN
                    SELECT * FROM Appointments WHERE AppointmentID = @AppointmentID;
                END;
            ");

            // Stored Procedure: Insert Appointment
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_InsertAppointment
                    @PatientID INT,
                    @AppointmentDate DATETIME2,
                    @DoctorName NVARCHAR(100)
                AS
                BEGIN
                    INSERT INTO Appointments (PatientID, AppointmentDate, DoctorName)
                    VALUES (@PatientID, @AppointmentDate, @DoctorName);
                    
                    SELECT SCOPE_IDENTITY() AS AppointmentID;
                END;
            ");

            // Stored Procedure: Update Appointment
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_UpdateAppointment
                    @AppointmentID INT,
                    @PatientID INT,
                    @AppointmentDate DATETIME2,
                    @DoctorName NVARCHAR(100)
                AS
                BEGIN
                    UPDATE Appointments
                    SET PatientID = @PatientID, AppointmentDate = @AppointmentDate, DoctorName = @DoctorName
                    WHERE AppointmentID = @AppointmentID;
                END;
            ");

            // Stored Procedure: Delete Appointment
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_DeleteAppointment
                    @AppointmentID INT
                AS
                BEGIN
                    DELETE FROM Appointments WHERE AppointmentID = @AppointmentID;
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop all stored procedures when rolling back the migration
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetAllAppointments;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetAppointmentById;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_InsertAppointment;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_UpdateAppointment;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_DeleteAppointment;");
        }
    }
}
