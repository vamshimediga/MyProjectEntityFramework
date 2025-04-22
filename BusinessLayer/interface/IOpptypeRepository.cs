using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IOpptypeRepository
    {
        Task<int> InsertOpptypeAsync(OpptypeDomainModel opptypeDomainModel);
        Task<OpptypeDomainModel> GetOpptypeByIdAsync(int opptypeId);
        Task<List<OpptypeDomainModel>> Get();

        Task<bool> Update(OpptypeDomainModel opptypeDomainModel);

    }
}
