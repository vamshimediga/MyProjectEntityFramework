using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class PatientRepository: IPatientRepository
    {


        public readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            var patients = await _context.Patients
                .FromSqlRaw("EXEC GetAllPatients")
                .ToListAsync();

            foreach (var patient in patients)
            {
                await _context.Entry(patient)
                    .Collection(p => p.Appointments)
                    .LoadAsync();
            }

            return patients;
        }


        public async Task<Patient> GetByIdAsync(int id)
        {
            try
            {
                var param = new SqlParameter("@PatientID", id);

                var result = await _context.Patients
                    .FromSqlRaw("EXEC GetPatientById @PatientID", param)
                    .ToListAsync(); // Materialize result

                return result.FirstOrDefault(); // Now safely use LINQ
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching patient with ID {id}.", ex);
            }
        }



        public async Task<int> InsertAsync(Patient patient)
        {
            var firstNameParam = new SqlParameter("@FirstName", patient.FirstName ?? (object)DBNull.Value);
            var lastNameParam = new SqlParameter("@LastName", patient.LastName ?? (object)DBNull.Value);
            var contactNumberParam = new SqlParameter("@ContactNumber", patient.ContactNumber ?? (object)DBNull.Value);

            return await _context.Database.ExecuteSqlRawAsync(
                "EXEC InsertPatient @FirstName, @LastName, @ContactNumber",
                firstNameParam, lastNameParam, contactNumberParam);
        }

        public async Task<int> UpdateAsync(Patient patient)
        {
            var idParam = new SqlParameter("@PatientID", patient.PatientID);
            var firstNameParam = new SqlParameter("@FirstName", patient.FirstName ?? (object)DBNull.Value);
            var lastNameParam = new SqlParameter("@LastName", patient.LastName ?? (object)DBNull.Value);
            var contactNumberParam = new SqlParameter("@ContactNumber", patient.ContactNumber ?? (object)DBNull.Value);

            return await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdatePatient @PatientID, @FirstName, @LastName, @ContactNumber",
                idParam, firstNameParam, lastNameParam, contactNumberParam);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var param = new SqlParameter("@PatientID", id);

            return await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeletePatient @PatientID", param);
        }
    }
}
