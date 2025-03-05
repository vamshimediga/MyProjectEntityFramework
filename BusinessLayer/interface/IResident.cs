using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IResident
    {
        Task<List<Resident>> GetResidentsAsync();

        Task<Resident> GetResidentByIdAsync(int id);

        Task Insert(Resident resident);

        Task Update(Resident resident);

        Task Delete(int id);
    }
}
