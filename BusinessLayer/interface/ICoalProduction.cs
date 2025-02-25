using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ICoalProduction
    {
        Task<List<CoalProduction>> GetCoalProductions();

        Task<CoalProduction> GetCoalProduction(int  id);

        Task Insert(CoalProduction coalProduction);

        Task Update(CoalProduction coalProduction);

        Task Delete(int id);
    }
}
