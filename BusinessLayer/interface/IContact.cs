using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IContact
    {
        Task<List<Contact>> GetContacts();

        Task<Contact> GetContactById(int id);   

        Task<bool> Insert(Contact contact);

        Task<bool> Update(Contact contact);

        Task<bool> Delete(int id);
    }
}
