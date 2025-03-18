using ISMIEEmploymentApp.Data;
using ISMIEEmploymentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ISMIEEmploymentApp.Repository
{
    public class CandidateApplicationRepository : ICandidateApplicationRepository
    {
        private readonly ApplicationDbContext _ctx;

        public CandidateApplicationRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }        

        public async Task AddAsync(Candidate candidate)
        {
            await _ctx.Candidates.AddAsync(candidate);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var candidate = await _ctx.Candidates.FindAsync(id);
            if (candidate != null)
            {
                _ctx.Candidates.Remove(candidate);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await _ctx.Candidates.ToListAsync();
        }

        public async Task<Candidate?> GetByIdAsync(int id)
        {
            return await _ctx.Candidates.FindAsync(id);
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _ctx.Candidates.Update(candidate);
            await _ctx.SaveChangesAsync();
        }
    }
}
