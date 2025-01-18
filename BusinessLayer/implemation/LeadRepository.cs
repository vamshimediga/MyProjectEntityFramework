using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public Task<bool> Update(Lead newLead)
        {
            throw new NotImplementedException();
        }
    }
}
