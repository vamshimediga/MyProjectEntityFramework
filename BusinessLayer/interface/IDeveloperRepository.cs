using Entities;


namespace BusinessLayer
{
    public interface IDeveloperRepository
    {
        Task<List<DeveloperDomainModel>> GetAll();
        Task<DeveloperDomainModel> GetById(int id);
        Task<int> Add(DeveloperDomainModel developer);
        Task<bool> Update(DeveloperDomainModel developer);
        Task<bool> Delete(int id);
    }
}
