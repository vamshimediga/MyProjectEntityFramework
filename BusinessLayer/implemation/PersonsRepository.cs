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
    public class PersonsRepository : IPersons
    {
        private readonly ApplicationDbContext _context;

        public PersonsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // Insert Person - Calls stored procedure to insert a person and return the PersonId
        public async Task<int> Insert(Person person)
        {
            var idParam = new Microsoft.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var nameParam = new Microsoft.Data.SqlClient.SqlParameter("@Name", person.Name ?? (object)DBNull.Value);
            var activeParam = new Microsoft.Data.SqlClient.SqlParameter("@Active", person.Active);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[InsertPerson] @Name, @Active, @Id OUTPUT",
                nameParam, activeParam, idParam
            );

            return (int)idParam.Value;
        }

        // Update Person - Calls stored procedure to update a person and return the updated PersonId
        public async Task<int> Update(Person person)
        {
            var updatedPersonIdParam = new Microsoft.Data.SqlClient.SqlParameter("@UpdatedPersonId", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var personIdParam = new Microsoft.Data.SqlClient.SqlParameter("@PersonId", person.PersonId);
            var nameParam = new Microsoft.Data.SqlClient.SqlParameter("@Name", person.Name ?? (object)DBNull.Value);
            var activeParam = new Microsoft.Data.SqlClient.SqlParameter("@Active", person.Active);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[UpdatePerson] @PersonId, @Name, @Active, @UpdatedPersonId OUTPUT",
                personIdParam, nameParam, activeParam, updatedPersonIdParam
            );

            return (int)updatedPersonIdParam.Value;
        }

        // Delete Person - Calls stored procedure to delete a person and return the deleted PersonId
        public async Task<int> Delete(int id)
        {
            var deletedPersonIdParam = new Microsoft.Data.SqlClient.SqlParameter("@DeletedPersonId", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var personIdParam = new Microsoft.Data.SqlClient.SqlParameter("@PersonId", id);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[DeletePerson] @PersonId, @DeletedPersonId OUTPUT",
                personIdParam, deletedPersonIdParam
            );

            return (int)deletedPersonIdParam.Value;
        }

        // Get all Persons - Calls stored procedure to fetch all persons
        public async Task<List<Person>> GetPeople()
        {
            return await _context.Persons.FromSqlRaw("EXEC [dbo].[GetAllPersons]").ToListAsync();
        }

        // Get Person by ID - Calls stored procedure to fetch a person by ID
        public async Task<Person> GetPerson(int id)
        {
            var personParam = new SqlParameter("@PersonId", id);

         
            var sql = "EXEC [dbo].[GetPersonById] @PersonId= {0}";

            // Execute the stored procedure and get the result as a list
            Person  person = _context.Persons
                .FromSqlRaw(sql, personParam)  // Pass the parameter to the SQL query
                .AsEnumerable()  // Move the query to the client side (client-side composition)
                .FirstOrDefault(); // Get the first student or null if not found

            // Optionally, load related entities (like Institute) if needed
            if (person != null)
            {
                // Explicitly load the related Institute navigation property
                await _context.Entry(person)
                    .Collection(p => p.Passports)
                    .LoadAsync();
            }

            return person;
        }
    }
}
