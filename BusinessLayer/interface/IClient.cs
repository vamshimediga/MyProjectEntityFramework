using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IClient
    {
        Task<List<Client>> GetClients();

        Task<Client> GetClient(int id);
        Task insert(Client client);
        Task update(Client client);
        Task delete(int id);
       
    }
}
