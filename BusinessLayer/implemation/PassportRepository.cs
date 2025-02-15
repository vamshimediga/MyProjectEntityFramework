using BusinessLayer;
using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PassportRepository :IPassport
{
    private readonly ApplicationDbContext _context;

    public PassportRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ Get all passports
    public async Task<List<Passport>> GetPassports()
    {
        List<Passport> passports = await _context.Passports.FromSqlRaw("EXEC dbo.GetAllPassports").ToListAsync();
        foreach (var passport in passports)
        {
            await _context.Entry(passport)
                .Reference(p => p.Person)  // Load the Contacts navigation property
                .LoadAsync();
        }

        return passports;
    }

    // ✅ Get passport by ID
    public async Task<Passport> GetPassportById(int id)
    {
        var param = new SqlParameter("@PassportId", id);
        return await _context.Passports
            .FromSqlRaw("EXEC dbo.GetPassportById @PassportId", param)
            .FirstOrDefaultAsync();
    }

    // ✅ Insert passport
    public async Task<int> insert(Passport passport)
    {
        var passportNumberParam = new SqlParameter("@PassportNumber", passport.PassportNumber);
        var personIdParam = new SqlParameter("@PersonId", passport.PersonId);
        var outputParam = new SqlParameter("@portId", System.Data.SqlDbType.Int)
        {
            Direction = System.Data.ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC dbo.InsertPassport @PassportNumber, @PersonId, @portId OUTPUT",
            passportNumberParam, personIdParam, outputParam);

        return (int)outputParam.Value;
    }

    // ✅ Update passport
    public async Task<int> update(Passport passport)
    {
        var passportIdParam = new SqlParameter("@PassportId", passport.PassportId);
        var passportNumberParam = new SqlParameter("@PassportNumber", passport.PassportNumber);
        var personIdParam = new SqlParameter("@PersonId", passport.PersonId);
        var outputParam = new SqlParameter("@UpdatedPassportId", System.Data.SqlDbType.Int)
        {
            Direction = System.Data.ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC dbo.UpdatePassport @PassportId, @PassportNumber, @PersonId, @UpdatedPassportId OUTPUT",
            passportIdParam, passportNumberParam, personIdParam, outputParam);

        return (int)outputParam.Value;
    }

    // ✅ Delete passport
    public async Task<int> delete(int id)
    {
        var passportIdParam = new SqlParameter("@PassportId", id);
        var outputParam = new SqlParameter("@DeletedPassportId", System.Data.SqlDbType.Int)
        {
            Direction = System.Data.ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC dbo.DeletePassport @PassportId, @DeletedPassportId OUTPUT",
            passportIdParam, outputParam);

        return (int)outputParam.Value;
    }

}
