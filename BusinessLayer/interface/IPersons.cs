using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IPersons
    {
        Task<List<Person>> GetPeople();

        Task<Person> GetPerson(int id);

        Task<int> Insert(Person person);

        Task<int> Update(Person person);

        Task<int> Delete(int id);
    }
}
