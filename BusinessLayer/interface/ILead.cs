using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ILead
    {
        Task<List<Lead>> leads();

        Task<Lead> Lead(int id);

        Task<bool> Insert(Lead newLead);

        Task<bool> Update(Lead newLead);

        Task<bool> Delete(int id);
    }
}
