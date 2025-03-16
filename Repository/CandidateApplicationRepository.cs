using ISMIEEmploymentApp.Models;

namespace ISMIEEmploymentApp.Repository
{
    public class CandidateApplicationRepository : ICandidateApplicationRepository
    {
        private readonly List<Candidate> _application = new();

        public void Add(Candidate application)
        {
            _application.Add(application);
        }

        public IEnumerable<Candidate> GetAll()
        {
            return _application;
        }
    }
}
