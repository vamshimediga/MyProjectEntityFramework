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
    public class LeadRepository : ILead
    {
        private readonly ApplicationDbContext _context;

        public LeadRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(int id)
        {
            var resultParameter = new SqlParameter("@Result", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[Leads_Delete] @LeadID, @Result OUTPUT",
                new SqlParameter("@LeadID", id),
                resultParameter
            );

            // Get the result from the output parameter
            bool isSuccess = (bool)resultParameter.Value;
            return isSuccess;
        }

        public async Task<bool> Insert(Lead lead)
        {
            // Define the output parameter
            var resultParameter = new SqlParameter("@Result", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[Leads_Insert] @FirstName, @LastName, @Email, @Result OUT",
                new SqlParameter("@FirstName", lead.FirstName ?? (object)DBNull.Value),
                new SqlParameter("@LastName", lead.LastName ?? (object)DBNull.Value),
                new SqlParameter("@Email", lead.Email ?? (object)DBNull.Value),
                resultParameter
            );

            // Retrieve the result from the output parameter
            bool result = (bool)resultParameter.Value;
            return result;
        }


        public async Task<Lead> Lead(int id)
        {
            // Fetch the lead by executing the stored procedure
            var lead =  _context.Leads.FromSqlRaw("EXEC [dbo].[Leads_GetById] @LeadID = {0}", id)
                                      .AsEnumerable()  // Perform client-side composition
                                      .FirstOrDefault();

            // If lead is found, load the related Contacts collection
            if (lead != null)
            {
                // Load the Contacts navigation property asynchronously
                await _context.Entry(lead)
                              .Collection(l => l.Contacts)  // Contacts is a navigation property
                              .LoadAsync();
            }

            return lead;
        }


        public async Task<List<Lead>> leads()
        {
            List<Lead> leads = await _context.Leads
       .FromSqlInterpolated($"EXEC [dbo].[Leads_GetAll]")
       .ToListAsync();

            // Load the Contacts collection for each Lead
            foreach (var lead in leads)
            {
                await _context.Entry(lead)
                    .Collection(l => l.Contacts)  // Load the Contacts navigation property
                    .LoadAsync();
            }

            return leads;
        }

        public async Task<bool> Update(Lead lead)
        {
            // Define the output parameter
            var resultParameter = new SqlParameter("@Result", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };

            // Execute the stored procedure
           await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[Leads_Update] @LeadID, @FirstName, @LastName, @Email, @Result OUT",
                new SqlParameter("@LeadID", lead.LeadID),
                new SqlParameter("@FirstName", lead.FirstName),
                new SqlParameter("@LastName", lead.LastName),
                new SqlParameter("@Email", lead.Email),
                resultParameter
            );

            // Retrieve the result from the output parameter
            bool result = (bool)resultParameter.Value;
            return result;
        }
    }
}
