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
        public Task<bool> Delete(Lead newLead)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(Lead newLead)
        {
            throw new NotImplementedException();
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
