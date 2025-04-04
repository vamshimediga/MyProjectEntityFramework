using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using Data.Repositories;
using Microsoft.Data.SqlClient;
using BusinessLayer;
using Entities;

namespace Data.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

      

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            // Fetch appointments using a stored procedure
            List<Entities.Appointment> appointments = await _context.Appointments
                .FromSqlRaw("EXEC sp_GetAllAppointments")
                .ToListAsync();

            // Explicitly load Patient reference for each Appointment
            foreach (Entities.Appointment appointment in appointments)
            {
                await _context.Entry(appointment)
                    .Reference(a => a.Patient) // Assuming Appointment has a Patient navigation property
                    .LoadAsync();
            }

            return appointments;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            try
            {
                var appointmentIdParam = new SqlParameter("@AppointmentID", appointmentId);

                var appointments = await _context.Appointments
                    .FromSqlRaw("EXEC sp_GetAppointmentById @AppointmentID", appointmentIdParam)
                    .ToListAsync(); // Fetch all results first

                return appointments.FirstOrDefault(); // Get the first result
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching appointment by ID {appointmentId}: {ex.Message}");
                throw new Exception("An error occurred while retrieving the appointment.", ex);
            }
        }



        public async Task<int> InsertAppointmentAsync(Appointment appointment)
        {
            try
            {
                var patientIdParam = new SqlParameter("@PatientID", appointment.PatientID);
                var appointmentDateParam = new SqlParameter("@AppointmentDate", appointment.AppointmentDate);
                var doctorNameParam = new SqlParameter("@DoctorName", appointment.DoctorName);

                var outputParam = new SqlParameter
                {
                    ParameterName = "@NewAppointmentID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_InsertAppointment @PatientID, @AppointmentDate, @DoctorName, @NewAppointmentID OUTPUT",
                    patientIdParam, appointmentDateParam, doctorNameParam, outputParam);

                return (int)outputParam.Value;
            }
            catch (Exception ex)
            {
                // Log the error (replace this with your logging mechanism)
                Console.WriteLine($"Error inserting appointment: {ex.Message}");

                // Optionally, rethrow the exception or return an error code
                throw new Exception("An error occurred while inserting the appointment.", ex);
            }
        }


        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            var appointmentIdParam = new SqlParameter("@AppointmentID", appointment.AppointmentID);
            var patientIdParam = new SqlParameter("@PatientID", appointment.PatientID);
            var appointmentDateParam = new SqlParameter("@AppointmentDate", appointment.AppointmentDate);
            var doctorNameParam = new SqlParameter("@DoctorName", appointment.DoctorName);

            var affectedRows = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateAppointment @AppointmentID, @PatientID, @AppointmentDate, @DoctorName",
                appointmentIdParam, patientIdParam, appointmentDateParam, doctorNameParam);

            return affectedRows > 0;
        }

        public async Task<bool> DeleteAppointmentAsync(int appointmentId)
        {
            var appointmentIdParam = new SqlParameter("@AppointmentID", appointmentId);

            var affectedRows = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DeleteAppointment @AppointmentID", appointmentIdParam);

            return affectedRows > 0;
        }
    }
}
