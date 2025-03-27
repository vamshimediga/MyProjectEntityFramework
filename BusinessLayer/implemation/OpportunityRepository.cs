using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class OpportunityRepository : IOpportunity
    {
        private readonly ApplicationDbContext _context;

        public OpportunityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Opportunity>> GetAllOpportunitiesAsync()
        {
            return await _context.Opportunities
                .FromSqlRaw("EXEC GetAllOpportunities")
                .ToListAsync();
        }

        public async Task<Opportunity> GetOpportunityByIdAsync(int opportunityId)
        {
            var param = new SqlParameter("@OpportunityID", opportunityId);

            return _context.Opportunities
                .FromSqlRaw("EXEC GetOpportunityByID @OpportunityID", param)
                .AsNoTracking()
                .AsEnumerable()  // ✅ Convert to IEnumerable before applying LINQ
                .FirstOrDefault();
        }


        public async Task<int> InsertOpportunityAsync(Opportunity opportunity)
        {
            try
            {
                var newOpportunityIdParam = new SqlParameter("@NewOpportunityID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                var parameters = new[]
                {
            new SqlParameter("@OpportunityName", opportunity.OpportunityName ?? (object)DBNull.Value),
            new SqlParameter("@EstimatedValue", opportunity.EstimatedValue),
            new SqlParameter("@CloseDate", opportunity.CloseDate),
            new SqlParameter("@Stage", opportunity.Stage ?? (object)DBNull.Value),
            newOpportunityIdParam
        };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC InsertOpportunity @OpportunityName, @EstimatedValue, @CloseDate, @Stage, @NewOpportunityID OUTPUT",
                    parameters
                );

                // Ensure the Output Parameter is properly set and not null
                return newOpportunityIdParam.Value != DBNull.Value ? Convert.ToInt32(newOpportunityIdParam.Value) : 0;
            }
            catch (SqlException sqlEx) // Handle SQL-related errors
            {
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw new Exception("An error occurred while inserting the opportunity into the database.", sqlEx);
            }
            catch (Exception ex) // Handle other unexpected errors
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw new Exception("An unexpected error occurred while processing the request.", ex);
            }
        }



        public async Task<bool> UpdateOpportunityAsync(Opportunity opportunity)
        {
            var parameters = new[]
            {
            new SqlParameter("@OpportunityID", opportunity.OpportunityID),
            new SqlParameter("@OpportunityName", opportunity.OpportunityName),
            new SqlParameter("@EstimatedValue", opportunity.EstimatedValue),
            new SqlParameter("@CloseDate", opportunity.CloseDate),
            new SqlParameter("@Stage", opportunity.Stage)
        };

            var affectedRows = await _context.Database.ExecuteSqlRawAsync("EXEC UpdateOpportunity @OpportunityID, @OpportunityName, @EstimatedValue, @CloseDate, @Stage", parameters);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteOpportunityAsync(int opportunityId)
        {
            var param = new SqlParameter("@OpportunityID", opportunityId);
            var affectedRows = await _context.Database.ExecuteSqlRawAsync("EXEC DeleteOpportunity @OpportunityID", param);
            return affectedRows > 0;
        }
    }

}
