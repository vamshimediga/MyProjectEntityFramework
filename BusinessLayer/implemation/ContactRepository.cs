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
    public class ContactRepository : IContact
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(int id)
        {

            var resultParam = new SqlParameter("@Result", System.Data.SqlDbType.Bit)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var contactIdParam = new SqlParameter("@ContactID", id);

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[Contacts_Delete] @ContactID, @Result OUT",
                contactIdParam,
                resultParam
            );

            // Return the result (whether the delete was successful)
            return (bool)resultParam.Value;
        }

        public async Task<Contact> GetContactById(int id)
        {
            Contact contact = _context.Contacts
        .FromSqlRaw("EXEC [dbo].[Contacts_GetById] @ContactID = {0}", id)
        .AsEnumerable()  // Force client-side evaluation
        .FirstOrDefault();  // Get the first result or null if no match is found

            // If contact is found, load the related Lead navigation property
            if (contact != null)
            {
                // Load the Lead navigation property asynchronously
                await _context.Entry(contact)
                    .Reference(c => c.Lead)  // Lead is a reference navigation property in Contact
                    .LoadAsync();  // Explicitly load the Lead reference
            }

            return contact;
        }

        public async Task<List<Contact>> GetContacts()
        {
            List<Contact> contacts = await _context.Contacts
           .FromSqlRaw("EXEC [dbo].[Contacts_Get]")
           .ToListAsync();  // Execute the stored procedure

            // Then, load the related Lead entities for each contact
            foreach (var contact in contacts)
            {
                await _context.Entry(contact)
                    .Reference(c => c.Lead)   // Load the related Lead entity
                    .LoadAsync();             // Asynchronously load the Lead
            }

            return contacts;
        }

        public async Task<bool> Insert(Contact contact)
        {
            var contactNameParam = new SqlParameter("@ContactName", SqlDbType.NVarChar)
            {
                Value = contact.ContactName ?? (object)DBNull.Value // Handle null values if necessary
            };

            var contactPhoneParam = new SqlParameter("@ContactPhone", SqlDbType.NVarChar)
            {
                Value = contact.ContactPhone ?? (object)DBNull.Value // Handle null values if necessary
            };

            var leadIdParam = new SqlParameter("@LeadID", SqlDbType.Int)
            {
                Value = contact.LeadID
            };

            var resultParam = new SqlParameter("@Result", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };

            // Execute the stored procedure with parameters
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[Contacts_Insert] @ContactName, @ContactPhone, @LeadID, @Result OUT",
                contactNameParam, contactPhoneParam, leadIdParam, resultParam);

            // Return the value of the output parameter
            return (bool)resultParam.Value;
        }

        public async Task<bool> Update(Contact contact)
        {
            var resultParam = new SqlParameter("@Result", System.Data.SqlDbType.Bit)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var contactIdParam = new SqlParameter("@ContactID", contact.ContactID);
            var contactNameParam = new SqlParameter("@ContactName", contact.ContactName);
            var contactPhoneParam = new SqlParameter("@ContactPhone", contact.ContactPhone);
            var leadIdParam = new SqlParameter("@LeadID", contact.LeadID);

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[Contacts_Update] @ContactID, @ContactName, @ContactPhone, @LeadID, @Result OUT",
                contactIdParam,
                contactNameParam,
                contactPhoneParam,
                leadIdParam,
                resultParam
            );

            // Check if the result was successful
            return (bool)resultParam.Value;
        }
    }
}
