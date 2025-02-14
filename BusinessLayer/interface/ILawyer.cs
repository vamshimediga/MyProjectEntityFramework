using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ILawyer
   {

        Task<IEnumerable<Lawyer>> GetAllLawyers();
        Task<Lawyer> GetLawyerById(int id);
        Task AddLawyer(Lawyer lawyer);
        Task UpdateLawyer(Lawyer lawyer);
        Task DeleteLawyer(int id);
    }
}
