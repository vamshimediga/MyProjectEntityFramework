using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ICompanyTypeRepository
    {
        Task<IEnumerable<CompanyTypeDomainModel>> GetAllAsync();
        Task<CompanyTypeDomainModel> GetByIdAsync(int id);
        Task<int> InsertAsync(CompanyTypeDomainModel companyType);
        Task<int> UpdateAsync(CompanyTypeDomainModel companyType);
        Task<int> DeleteAsync(int id);
    }
}
