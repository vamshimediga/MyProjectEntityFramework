using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class ApartmentRepository:IApartment
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConnection _dbConnection;

       // private readonly ApplicationDbContext _context;

        public ApartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Apartments (Stored Procedure)
        public async Task<IEnumerable<Apartment>> GetAllApartmentsAsync()
        {
            return await _context.Apartments
                .FromSqlRaw("EXEC GetAllApartments")
                .ToListAsync();
        }

        // Get Apartment by ID (Stored Procedure)
        public async Task<Apartment> GetApartmentByIdAsync(int apartmentId)
        {
            var param = new SqlParameter("@ApartmentID", apartmentId);

            return  _context.Apartments
                .FromSqlRaw("EXEC GetApartmentById @ApartmentID", param)
                .AsEnumerable()
                .FirstOrDefault();
        }

        // Insert Apartment (Stored Procedure)
        public async Task InsertApartmentAsync(Apartment apartment)
        {
            var apartmentNameParam = new SqlParameter("@ApartmentName", SqlDbType.NVarChar) { Value = apartment.ApartmentName };
            var addressParam = new SqlParameter("@Address", SqlDbType.NVarChar) { Value = apartment.Address };
            var totalUnitsParam = new SqlParameter("@TotalUnits", SqlDbType.Int) { Value = apartment.TotalUnits };
            var residentId = new SqlParameter("@ResidentId", SqlDbType.Int) { Value = apartment.ResidentId };
            var outputIdParam = new SqlParameter("@ApartmentID", SqlDbType.Int) { Direction = ParameterDirection.Output };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC InsertApartment @ApartmentName, @Address, @TotalUnits,@ResidentId; SET @ApartmentID = SCOPE_IDENTITY();",
                apartmentNameParam, addressParam, totalUnitsParam, residentId,outputIdParam
            );
            // Returns affected rows, but for identity, you'd need to modify the stored procedure
        }

        // Update Apartment (Stored Procedure)
        public async Task UpdateApartmentAsync(Apartment apartment)
        {
            var rowsAffected = await _context.Database
                .ExecuteSqlRawAsync("EXEC UpdateApartment @p0, @p1, @p2, @p3,@p4",
                    apartment.ApartmentID, apartment.ApartmentName, apartment.Address, apartment.TotalUnits, apartment.ResidentId);

         //   return rowsAffected > 0;
        }

        // Delete Apartment (Stored Procedure)
        public async Task DeleteApartmentAsync(int apartmentId)
        {
            var rowsAffected = await _context.Database
                .ExecuteSqlRawAsync("EXEC DeleteApartment @p0", apartmentId);

           // return rowsAffected > 0;
        }
    }
}
