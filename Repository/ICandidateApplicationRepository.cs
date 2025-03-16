using ISMIEEmploymentApp.Models;

namespace ISMIEEmploymentApp.Repository
{
    public interface ICandidateApplicationRepository
    {
        void Add(Candidate application);
        IEnumerable<Candidate> GetAll();
    }
}
