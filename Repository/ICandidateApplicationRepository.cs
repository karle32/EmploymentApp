using ISMIEEmploymentApp.Models;

namespace ISMIEEmploymentApp.Repository
{
    public interface ICandidateApplicationRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<Candidate?> GetByIdAsync(int id);
        Task AddAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);
        Task DeleteAsync(int id);
    }
}
